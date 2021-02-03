/****************************************************************************
*  Файл: SecurityUserRepository.cs
*  Автор: Данилов А.В.
*  Дата создания: 01.02.2021
*  Назначение: Определение класса SecurityUserRepository
****************************************************************************/

using DBRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBRepository.Repositories
{
    /// <summary>
    /// Реализация интерфейса доступа до пользователей системы безопасности
    /// </summary>
    public class SecurityUserRepository : BaseRepository, ISecurityUserRepository
    {

        public SecurityUserRepository(string connectionString, IRepositoryContextFactory contextFactory) 
            : base(connectionString, contextFactory) { }

        public async Task<IList<SecurityUser>> GetAllUsers()
        {
            IList<SecurityUser> result = null;
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                result = await context.SecurityUsers.ToListAsync();
            }
            return result;
        }

        public SecurityUser GetUserByLogin(string login)
        {
            SecurityUser result = null;

            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                result = context.SecurityUsers
                    .Include(x => x.Roles)
                    .ThenInclude(c => c.ClaimsDefenitions)
                    .Where(x => x.Login == login).FirstOrDefault();
            }

            return result;
        }

        public async Task SaveListAsync(List<SecurityUser> users)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                // Проверим существуют ли вложенные записи в БД и если да, то обновим ссылки из текущего контекста
                users.ForEach(user =>
                {
                    var allRoleIds = user.Roles.Select(x => x.RoleId);
                    var existsRoles = context.Roles.Where(x => allRoleIds.Contains(x.RoleId));

                    var newRoles = new List<Role>();

                    user.Roles.ForEach(role =>
                    {
                        if (!existsRoles.Select(x => x.RoleId).Contains(role.RoleId))
                        {
                            newRoles.Add(role);
                        }
                    });

                    user.Roles = existsRoles.ToList().Concat(newRoles).ToList();
                });

                context.SecurityUsers.AddRange(users);
                await context.SaveChangesAsync();
            }
        }
    }
}
