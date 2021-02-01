/****************************************************************************
*  Файл: AppUser.cs
*  Автор: Данилов А.В.
*  Дата создания: 02.02.2021
*  Назначение: Определение класса AppUser
****************************************************************************/

using Models.Security;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Common
{
    /// <summary>
    /// Пользователь системы в контексте текущего приложения
    /// </summary>
    public class AppUser
    {
        // Первичный ключ
        [Key]
        public Guid AppUserId { get; set; }

        // Связь с пользователем системы безопасности
        public Guid SecurityUserId { get; set; }

        // Никнейм
        [MaxLength(256)]
        [Required]
        public string Nickname { get; set; }

        // Имя
        [MaxLength(256)]
        [Required]
        public string FirstName { get; set; }

        // Фамилия
        [MaxLength(256)]
        [Required]
        public string SecondName { get; set; }

        // Отчество?
        [MaxLength(256)]
        public string MiddleName { get; set; }

        // Зарегестрирован
        public DateTime Registrated { get; set; }

        // Дата рождения
        public DateTime Birthday { get; set; }

        // Пользователь системы безопасности
        [ForeignKey("SecurityUserId")]
        public SecurityUser SecurityUser { get; set; }

    }
}
