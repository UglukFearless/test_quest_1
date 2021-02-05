/****************************************************************************
*  Файл: TokenHelper.cs
*  Автор: Данилов А.В.
*  Дата создания: 05.02.2021
*  Назначение: Определение класса TokenHelper
****************************************************************************/

using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace backend.Security.Helpers
{
    /// <summary>
    /// Класс для операций с токеном
    /// </summary>
    public static class TokenHelper
    {
        /// <summary>
        /// Гененрирует токен на основе полученных данных
        /// </summary>
        /// <param name="identity">данные для токена</param>
        /// <param name="deadTime">время гибели токена</param>
        /// <returns>Токен безопасности</returns>
        public static string GenerateToken(ClaimsIdentity identity, out DateTime deadTime)
        {
            var now = DateTime.UtcNow;
            deadTime = now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME));
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: deadTime,
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
