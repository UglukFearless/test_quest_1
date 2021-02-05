/****************************************************************************
*  Файл: InvalidAuthException.cs
*  Автор: Данилов А.В.
*  Дата создания: 05.02.2021
*  Назначение: Определение класса InvalidAuthException
****************************************************************************/

using System;

namespace backend.Security.Exceptions
{
    /// <summary>
    /// Исключение оповещающе об ошибке валидации логина и пароля пользователя
    /// </summary>
    public class InvalidAuthException : Exception
    {
        public InvalidAuthException(string message) : base(message) {}
    }
}
