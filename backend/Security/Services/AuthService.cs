/****************************************************************************
*  Файл: AuthService.cs
*  Автор: Данилов А.В.
*  Дата создания: 05.02.2021
*  Назначение: Определение класса AuthService
****************************************************************************/

using backend.Security.Exceptions;
using backend.Security.Helpers;
using DBRepository.Interfaces;
using DBRepository.Interfaces.Common;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Models.Common;
using Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace backend.Security.Services
{
    /// <summary>
    /// Сервис предоставляющий методы аутетификации
    /// </summary>
    public class AuthService
    {
        #region Properties

        private readonly ILogger<AuthService> _logger;

        private IAppUserRepository _userRepository;
        private IRefreshTokenRepository _refreshTokenRepository;

        #endregion Properties

        public AuthService(ILogger<AuthService> logger
                         , IAppUserRepository userRepository
                         , IRefreshTokenRepository refreshTokenRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        /// <summary>
        /// Метод осуществляет аутентификацию данных и генерирует токены
        /// </summary>
        /// <param name="login">Логин запроса</param>
        /// <param name="password">Пароль запроса</param>
        /// <returns>Набор даннях для передачи клиенту</returns>
        public async Task<(string, DateTime, string, DateTime , AppUser)> Login(string login, string password)
        {

            Guid session = Guid.NewGuid();
            AppUser user = _userRepository.GetUserByLogin(login);

            if (user == null || user.SecurityUser == null)
            {
                throw new InvalidAuthException("Invalid login or password.");
            }

            // набор данных для кодирования в токен
            var identity = ClaimsIdentityHelper.GetIdentityByLogin(user.SecurityUser, password, session);

            if (identity == null)
            {
                throw new InvalidAuthException("Invalid login or password.");
            }

            // генерация токенов
            var encodedJwt = TokenHelper.GenerateToken(identity, out var deadTime);
            var refreshToken = await this.GenerateRefreshTokenForUser(user.SecurityUser, session);

            _logger.LogInformation(string.Format("Пользователь {0} прошёл аутентификацию. Время: {1}", user.Nickname, DateTime.Now));

            return (refreshToken.Token, refreshToken.TimeOfDeath, encodedJwt, deadTime, user);
        }

        /// <summary>
        /// Метод валидирует данные и удаляет токен обновления из БД
        /// </summary>
        /// <param name="user">контекст пользователя</param>
        /// <returns></returns>
        public async Task Logout(ClaimsPrincipal user)
        {
            var login = user.Identity.Name;

            var askUser = _userRepository.GetUserByLogin(login);

            if (askUser == null || askUser.SecurityUser == null)
                throw new SecurityTokenException("Invalid token");

            var sessionClaim = user.Claims.Where(x => x.Type == CustomClaimTypes.Session).FirstOrDefault();

            if (sessionClaim == null)
                throw new SecurityTokenException("Invalid token");

            var session = Guid.Parse(sessionClaim.Value);

            var savedRefreshToken = _refreshTokenRepository.GetByLoginAndSession(login, session);

            if (savedRefreshToken == null)
                throw new SecurityTokenException("Invalid refresh token");

            await _refreshTokenRepository.DeleteRefreshTokenAsync(savedRefreshToken.Token);
        }

        /// <summary>
        /// Обновляет токены
        /// </summary>
        /// <param name="token">старый токен</param>
        /// <param name="refreshToken">токен обновления</param>
        /// <returns>Данные для клиента</returns>
        public async Task<(string, DateTime, string, DateTime)> Refresh(string token, string refreshToken)
        {
            // получим данные для работы
            var principal = RefreshTokenHelper.GetPrincipalFromExpiredToken(token);
            var login = principal.Identity.Name;
            var askUser = _userRepository.GetUserByLogin(login);

            if (askUser == null || askUser.SecurityUser == null)
                throw new SecurityTokenException("Invalid token");

            var sessionClaim = principal.Claims.Where(x => x.Type == CustomClaimTypes.Session).FirstOrDefault();

            if (sessionClaim == null)
                throw new SecurityTokenException("Invalid token");

            // извлечем сессию - нужно, чтобы пользователь мог параллельно работать с разных устройств
            var session = Guid.Parse(sessionClaim.Value);

            // получим токен из БД
            var savedRefreshToken = _refreshTokenRepository.GetByTokenAndLoginAndSession(refreshToken, login, session);

            if (savedRefreshToken == null)
                throw new SecurityTokenException("Invalid refresh token");

            var previosDeadTime = savedRefreshToken.TimeOfDeath;

            if (previosDeadTime < DateTime.UtcNow)
                throw new SecurityTokenException("Invalid refresh token");

            // если все проверки прошли - сгенерируем новые данные для пользователя
            var identity = ClaimsIdentityHelper.GetIdentity(askUser.SecurityUser, savedRefreshToken.Session);
            var newEncodedToken = TokenHelper.GenerateToken(identity, out var deadTime);
            var newRefreshToken = RefreshTokenHelper.GenerateRefreshToken();

            // удалим старый токен
            await _refreshTokenRepository.DeleteRefreshTokenAsync(savedRefreshToken.Token);

            var newRefreshTokenObj = new RefreshToken
            {
                RefreshTokenId = Guid.NewGuid(),
                Token = newRefreshToken,
                TimeOfDeath = DateTime.UtcNow.AddMinutes(AuthOptions.REFRESH_LIFETIME),
                SecurityUserId = askUser.SecurityUser.SecurityUserId,
                Session = savedRefreshToken.Session,
            };

            await _refreshTokenRepository.SaveRefreshTokenAsync(newRefreshTokenObj);

            // Соберём данные и вернём контроллеру
            return (
                newRefreshTokenObj.Token,
                newRefreshTokenObj.TimeOfDeath,
                newEncodedToken,
                deadTime
                );
        }

        /// <summary>
        /// Генерирует токен обновления для пользователя и сессии
        /// </summary>
        /// <param name="user">пользователь системы безопасности</param>
        /// <param name="session">идентификатор сессии</param>
        /// <returns>Объект токена безопасности</returns>
        private async Task<RefreshToken> GenerateRefreshTokenForUser(SecurityUser user, Guid session)
        {
            var newRefreshToken = RefreshTokenHelper.GenerateRefreshToken();

            var newRefreshTokenObj = new RefreshToken
            {
                RefreshTokenId = Guid.NewGuid(),
                Token = newRefreshToken,
                TimeOfDeath = DateTime.UtcNow.AddMinutes(AuthOptions.REFRESH_LIFETIME),
                Session = session,
                SecurityUserId = user.SecurityUserId,
            };

            await _refreshTokenRepository.SaveRefreshTokenAsync(newRefreshTokenObj);

            return newRefreshTokenObj;
        }
    }
}
