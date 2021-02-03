/****************************************************************************
*  Файл: Permissions.cs
*  Автор: Данилов А.В.
*  Дата создания: 03.02.2021
*  Назначение: Определение класса Permissions
****************************************************************************/

namespace backend.Security
{
    /// <summary>
    /// Строковые константы определяющие кастомные типы параметров
    /// </summary>
    public class CustomClaimTypes
    {
        public const string Permission = "permission";

        public const string Session = "session";
    }
}
