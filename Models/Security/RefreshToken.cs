/****************************************************************************
*  Файл: RefreshToken.cs
*  Автор: Данилов А.В.
*  Дата создания: 01.02.2021
*  Назначение: Определение класса RefreshToken
****************************************************************************/

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Security
{
    public class RefreshToken
    {
        // Первичный ключ
        [Key]
        public Guid RefreshTokenId { get; set; }

        // Уникальный токен-ключ по которому осуществляется обновление токена безопасности
        [Required]
        public string Token { get; set; }

        // Время до которого действителен токен обновления
        public DateTime TimeOfDeath { get; set; }

        // Ссылка на пользователя, к которому относится данный токен
        public Guid SecurityUserId { get; set; }

        // Ключ идентификатор сессии по которой получен токен обновления
        public Guid Session { get; set; }

        // Пользователь для которого получен токен обновления
        [ForeignKey("SecurityUserId")]
        public SecurityUser User { get; set; }
    }
}
