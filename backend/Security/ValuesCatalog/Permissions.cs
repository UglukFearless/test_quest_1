/****************************************************************************
*  Файл: Permissions.cs
*  Автор: Данилов А.В.
*  Дата создания: 03.02.2021
*  Назначение: Определение класса Permissions
****************************************************************************/

namespace backend.Security
{
    /// <summary>
    /// Класс хранит строковые константы соответствующие разрешениям
    /// </summary>
    public static class Permissions
    {
        public static class Users
        {
            public const string Read = "Permissions.Users.Read";
            public const string ReadDeep = "Permissions.Users.Read.Deep";
            public const string Create = "Permissions.Users.Create";
            public const string Edit = "Permissions.Users.Edit";
            public const string Delete = "Permissions.Users.Delete";
            public const string EditSelf = "Permissions.Users.Edit.Self";
        }
    }
}
