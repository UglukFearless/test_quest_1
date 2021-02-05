/****************************************************************************
*  Файл: SecurityUser.cs
*  Автор: Данилов А.В.
*  Дата создания: 01.02.2021
*  Назначение: Определение класса SecurityUser
****************************************************************************/

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Security
{
    /// <summary>
    /// Основная информация о пользователе в контексте системы безопасности
    /// </summary>
    public class SecurityUser
    {
        // Первичный ключ
        [Key]
        public Guid SecurityUserId { get; set; }

        // Уникальный логин пользователя
        [Required]
        public string Login { get; set; }

        // Пароль безопасности (не сериализуется в json)
        [JsonIgnore]
        [Required]
        public string Password { get; set; }

        [JsonIgnore]
        // Список ролей пользователя, определяющих его разрешения
        public List<Role> Roles { get; set; }
    }
}
