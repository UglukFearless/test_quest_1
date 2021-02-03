/****************************************************************************
*  Файл: IAppUserRepository.cs
*  Автор: Данилов А.В.
*  Дата создания: 02.02.2021
*  Назначение: Определение интерфейса IAppUserRepository
****************************************************************************/

using Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBRepository.Interfaces.Common
{
    /// <summary>
    /// Интерфейс доступа к пользователям
    /// </summary>
    public interface IAppUserRepository
    {
        /// <summary>
        /// Получить список всех пользователей системы
        /// </summary>
        /// <returns>Список всех пользователей системы</returns>
        Task<IList<AppUser>> GetAllUsersAsync();

        /// <summary>
        /// Получить пользователя системы по его никнейму
        /// </summary>
        /// <param name="nickname">Никнейм пользователя</param>
        /// <returns>Пользователь системы</returns>
        AppUser GetUserByLogin(string nickname);

        /// <summary>
        /// Получить пользователя системы по его илентификатору
        /// </summary>
        /// <param name="id">идентификатор пользователя</param>
        /// <returns>Пользователь системы</returns>
        AppUser GetUserById(Guid id);

        /// <summary>
        /// Получить пользователя системы по идентификатору пользователя системы безопасности
        /// </summary>
        /// <param name="login">Идентификатор пользователя системы безопасности</param>
        /// <returns>Пользователь системы</returns>
        AppUser GetUserBySecurityId(Guid securityId);

        /// <summary>
        /// Сохранить список пользователей системы
        /// </summary>
        /// <param name="users">Список пользователей</param>
        /// <returns></returns>
        Task SaveListAsync(List<AppUser> users);

        /// <summary>
        /// Получить количество всех пользователей
        /// </summary>
        /// <returns></returns>
        int GetUsersCount();
    }
}
