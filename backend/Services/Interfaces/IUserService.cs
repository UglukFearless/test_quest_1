/****************************************************************************
*  Файл: IUserService.cs
*  Автор: Данилов А.В.
*  Дата создания: 02.02.2021
*  Назначение: Определение интерфейса IUserService
****************************************************************************/

using Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Interfaces
{
    /// <summary>
    ///  Интерфейс сервиса для работы с пользователями системы
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Проверить наличие пользователей в системе
        /// </summary>
        /// <returns></returns>
        bool CheckUserExists();

        /// <summary>
        /// Сохраняет список пользователей
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        Task SaveListAsync(List<AppUser> users);
    }
}
