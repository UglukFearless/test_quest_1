/****************************************************************************
*  Файл: RoleRepository.cs
*  Автор: Данилов А.В.
*  Дата создания: 01.02.2021
*  Назначение: Определение класса RoleRepository
****************************************************************************/

using DBRepository.Interfaces;
using Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBRepository.Repositories
{
    /// <summary>
    /// Реализация интерфейса доступа до ролей пользователей системы
    /// </summary>
    public class RoleRepository : BaseRepository, IRoleRepository
    {

        public RoleRepository(string connectionString, IRepositoryContextFactory contextFactory)
            : base(connectionString, contextFactory) { }

        public Role GetById(Guid roleId)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return context.Roles.Where(x => x.RoleId == roleId).FirstOrDefault();
            }
        }

        public async Task SaveListAsync(IList<Role> roles)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                context.Roles.AddRange(roles);
                await context.SaveChangesAsync();
            }
        }
    }
}
