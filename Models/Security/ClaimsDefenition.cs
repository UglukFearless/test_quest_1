/****************************************************************************
*  Файл: ClaimsDefenition.cs
*  Автор: Данилов А.В.
*  Дата создания: 01.02.2021
*  Назначение: Определение класса ClaimsDefenition
****************************************************************************/

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Security
{
    /// <summary>
    /// Разрешения, определяющие доступ к действиям с апи и разделам сайта
    /// </summary>
    public class ClaimsDefenition
    {
        // Первичный ключ
        [Key]
        public Guid ClaimsDefenitionId { get; set; }

        // Тип разрешения
        [Required]
        public string Type { get; set; }

        // Текстовое значение, определяющее разрешение
        [Required]
        public string Value { get; set; }

        // Ссылка на первичный ключ роли
        public Guid RoleId { get; set; }

        // Роль, к которой относится данное разрешение
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
    }
}
