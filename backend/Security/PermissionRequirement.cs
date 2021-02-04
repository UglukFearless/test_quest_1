/****************************************************************************
*  Файл: PermissionRequirement.cs
*  Автор: Данилов А.В.
*  Дата создания: 04.02.2021
*  Назначение: Определение класса PermissionRequirement
****************************************************************************/

using Microsoft.AspNetCore.Authorization;

namespace backend.Security
{
    /// <summary>
    /// "Требование" авторизации для политики на основе разрешений
    /// </summary>
    public class PermissionRequirement : IAuthorizationRequirement
    {
        /// <summary>
        ///  Строка разрешения
        /// </summary>
        public string Permission { get; private set; }

        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }
}
