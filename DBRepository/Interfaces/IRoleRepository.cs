/****************************************************************************
*  Файл: IRoleRepository.cs
*  Автор: Данилов А.В.
*  Дата создания: 01.02.2021
*  Назначение: Определение интерфейса IRoleRepository
****************************************************************************/

using Models.Security;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBRepository.Interfaces
{
    /// <summary>
    /// Интерфейс доступа до ролей пользователей системы
    /// </summary>
    public interface IRoleRepository
    {
        /// <summary>
        /// Сохранить список ролей пользователей в БД
        /// </summary>
        /// <param name="roles">Список ролей</param>
        /// <returns></returns>
        Task SaveList(List<Role> roles);

        /// <summary>
        /// Получить роль системы по идентификатору
        /// </summary>
        /// <param name="roleId">Уникальный идентификатор</param>
        /// <returns>Роль</returns>
        Role GetById(Guid roleId);
    }
}
