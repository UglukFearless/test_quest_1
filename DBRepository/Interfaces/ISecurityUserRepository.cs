/****************************************************************************
*  Файл: ISecurityUserRepository.cs
*  Автор: Данилов А.В.
*  Дата создания: 01.02.2021
*  Назначение: Определение интерфейса ISecurityUserRepository
****************************************************************************/

using Models.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBRepository.Interfaces
{
    /// <summary>
    /// Интерфейс доступа до пользователей системы безопасности
    /// </summary>
    public interface ISecurityUserRepository
    {
        /// <summary>
        /// Получить список всех пользователей системы безопасности
        /// </summary>
        /// <returns>Список всех пользователей системы безопасности</returns>
        Task<IList<SecurityUser>> GetAllUsers();

        /// <summary>
        /// Получить пользователя системы безопасности по его логину
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns>Пользователь системы безопасности</returns>
        SecurityUser GetUserByLogin(string login);

        /// <summary>
        /// Сохранить список пользователей системы безопасности
        /// </summary>
        /// <param name="users">Список пользователей</param>
        /// <returns></returns>
        Task SaveListAsync(List<SecurityUser> users);
    }
}
