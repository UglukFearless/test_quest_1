/****************************************************************************
*  Файл: IRefreshTokenRepository.cs
*  Автор: Данилов А.В.
*  Дата создания: 01.02.2021
*  Назначение: Определение интерфейса IRefreshTokenRepository
****************************************************************************/

using Models.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DBRepository.Interfaces
{
    /// <summary>
    /// Интерфейс доступа к токенам обновления
    /// </summary>
    public interface IRefreshTokenRepository
    {
        /// <summary>
        /// Сохранить токен обновления
        /// </summary>
        /// <param name="refreshToken">Токен обновления</param>
        /// <returns></returns>
        Task SaveRefreshToken(RefreshToken refreshToken);

        /// <summary>
        /// Удалить токен обновления
        /// </summary>
        /// <param name="token">Уникальная строка токена</param>
        /// <returns></returns>
        Task DeleteRefreshToken(string token);

        /// <summary>
        /// Получить токен обновления по уникальной строке, логину пользователя и сессии
        /// </summary>
        /// <param name="token"></param>
        /// <param name="login"></param>
        /// <param name="session"></param>
        /// <returns>Токен обновления</returns>
        RefreshToken GetByTokenAndLoginAndSession(string token, string login, Guid session);

        /// <summary>
        /// Получить токен обновления по логину пользователя и сессии
        /// </summary>
        /// <param name="login"></param>
        /// <param name="session"></param>
        /// <returns>Токен обновления</returns>
        RefreshToken GetByLoginAndSession(string login, Guid session);

        /// <summary>
        /// Удалить все токены, чье время жизни истекло
        /// </summary>
        Task DeleteOutdatedAsync();
    }
}
