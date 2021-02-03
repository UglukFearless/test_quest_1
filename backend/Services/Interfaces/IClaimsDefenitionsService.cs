/****************************************************************************
*  Файл: IRoleService.cs
*  Автор: Данилов А.В.
*  Дата создания: 03.02.2021
*  Назначение: Определение интерфейса IRoleService
****************************************************************************/

using Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса разрешений
    /// </summary>
    public interface IClaimsDefenitionsService
    {
        /// <summary>
        /// Сохраняет список разрешений
        /// </summary>
        /// <param name="cliams"></param>
        /// <returns></returns>
        Task SaveListAsync(IList<ClaimsDefenition> cliams);
    }
}
