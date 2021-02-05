/****************************************************************************
*  Файл: ClaimsIdentityHelper.cs
*  Автор: Данилов А.В.
*  Дата создания: 05.02.2021
*  Назначение: Определение класса ClaimsIdentityHelper
****************************************************************************/

using Models.Security;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace backend.Security.Helpers
{
    /// <summary>
    /// Помощник для работы с ClaimsIdentity
    /// </summary>
    public static class ClaimsIdentityHelper
    {
        /// <summary>
        /// Проверяет соответствия логина и пароля и подготовливает коллекцию ClaimsIdentity
        /// </summary>
        /// <param name="askUser">Пользователь системы безопасности (предполагаемый)</param>
        /// <param name="password">пароль</param>
        /// <param name="session">идентификатор сессии</param>
        /// <returns>Коллекция Claims</returns>
        public static ClaimsIdentity GetIdentityByLogin(SecurityUser askUser, string password, Guid session)
        {
            if (askUser != null && BCrypt.Net.BCrypt.Verify(password, askUser.Password))
            {
                return GetIdentity(askUser, session);
            }

            return null;
        }

        /// <summary>
        /// Подготавливает коллекцию Claims
        /// </summary>
        /// <param name="askUser">Пользователь системы безопасности (предполагаемый)</param>
        /// <param name="session">идентификатор сессии</param>
        /// <returns>Коллекция Claims</returns>
        public static ClaimsIdentity GetIdentity(SecurityUser askUser, Guid session)
        {
            // данные, которые есть у всех пользователей
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, askUser.Login),
                new Claim(CustomClaimTypes.Session, session.ToString()),
            };

            // добавим данные на основе хранящихся в БД разрешений
            askUser.Roles.ForEach(role =>
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role.Code));

                role.ClaimsDefenitions.ForEach(definition =>
                {
                    claims.Add(new Claim(definition.Type, definition.Value));
                });
            });

            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
    }
}
