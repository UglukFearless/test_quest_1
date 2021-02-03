/****************************************************************************
*  Файл: ISecurityUserService.cs
*  Автор: Данилов А.В.
*  Дата создания: 04.02.2021
*  Назначение: Определение интерфейса ISecurityUserService
****************************************************************************/

using Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса пользователей системы безопасности
    /// </summary>
    public interface ISecurityUserService
    {
        /// <summary>
        /// Сохраняет список пользователей системы безопасности
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        Task SaveListAsync(List<SecurityUser> users);
    }
}
