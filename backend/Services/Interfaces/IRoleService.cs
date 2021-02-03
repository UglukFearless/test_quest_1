/****************************************************************************
*  Файл: IRoleService.cs
*  Автор: Данилов А.В.
*  Дата создания: 03.02.2021
*  Назначение: Определение интерфейса IRoleService
****************************************************************************/

using Models.Security;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Services.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса ролей
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Сохраняет список ролей
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        Task SaveListAsync(IList<Role> roles);
    }
}
