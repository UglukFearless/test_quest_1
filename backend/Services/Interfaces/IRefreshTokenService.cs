/****************************************************************************
*  Файл: IRefreshTokenService.cs
*  Автор: Данилов А.В.
*  Дата создания: 05.02.2021
*  Назначение: Определение интерфейса IRefreshTokenService
****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса для работы с токенами обновления
    /// </summary>
    public interface IRefreshTokenService
    {
        /// <summary>
        /// Удалить все устаревшие токены обновления из БД
        /// </summary>
        /// <returns></returns>
        Task DeleteOutdatedAsync();
    }
}
