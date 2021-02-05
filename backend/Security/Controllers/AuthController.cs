/****************************************************************************
*  Файл: AuthController.cs
*  Автор: Данилов А.В.
*  Дата создания: 05.02.2021
*  Назначение: Определение класса AuthController
****************************************************************************/

using backend.Security.Exceptions;
using backend.Security.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Security.Controllers
{
    /// <summary>
    /// Контроллер обрабатывает запросы аутентификации
    /// </summary>
    [Controller]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {

        private readonly ILogger<AuthController> _logger;

        private AuthService _authService;

        public AuthController(ILogger<AuthController> logger, AuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns>Набор данных для доступа и авторизации</returns>
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(string login, string password)
        {
            try
            {
                var authData = await _authService.Login(login, password);

                var response = new
                {
                    refresh_token = authData.Item1,
                    refresh_token_dead = authData.Item2,
                    access_token = authData.Item3,
                    access_token_dead = authData.Item4,
                    user = authData.Item5,
                };

                string json = JsonConvert.SerializeObject(response);
                return Content(json, "application/json");
            } 
            catch (InvalidAuthException ex)
            {
                return BadRequest(new { errorText = ex.Message });
            }
        }

        /// <summary>
        /// Выход из системы
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("logout")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout(User);

            return Ok("Logout success");
        }

        /// <summary>
        /// Метод для обновления токенов (чтобы оставаться в системе)
        /// </summary>
        /// <param name="token"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> RefreshToken(string token, string refreshToken)
        {

            var responseData = await _authService.Refresh(token, refreshToken);

            return new ObjectResult(new
            {
                refresh_token = responseData.Item1,
                refresh_token_dead = responseData.Item2,
                access_token = responseData.Item3,
                access_token_dead = responseData.Item4
            });
        }

        /// <summary>
        /// Проверка авторизации
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("getlogin")]
        [HttpGet]
        public IActionResult GetLogin()
        {
            return Ok($"Ваш логин: {User.Identity.Name}");
        }
    }
}
