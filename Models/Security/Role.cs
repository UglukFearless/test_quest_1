/****************************************************************************
*  Файл: Role.cs
*  Автор: Данилов А.В.
*  Дата создания: 01.02.2021
*  Назначение: Определение класса Role
****************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Security
{
    /// <summary>
    /// Роль пользователя в системе безопасности
    /// </summary>
    public class Role
    {
        // Первичный ключ
        [Key]
        public Guid RoleId { get; set; }

        // Уникальный код роли
        [Required]
        public string Code { get; set; }

        // Название роли
        [Required]
        public string Name { get; set; }

        // Метка, необходимая при ассинхронном доступе и модицикации
        public Guid ConcurrencyStamp { get; set; }

        // Список разрешений роли
        public List<ClaimsDefenition> ClaimsDefenitions { get; set; }

        // Список пользователей относящихся к роли
        public List<SecurityUser> SecurityUsers { get; set; }
    }
}
