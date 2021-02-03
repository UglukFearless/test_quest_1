/****************************************************************************
*  Файл: AppUserRepository.cs
*  Автор: Данилов А.В.
*  Дата создания: 03.02.2021
*  Назначение: Определение класса AppUserRepository
****************************************************************************/

using DBRepository.Interfaces;
using DBRepository.Interfaces.Common;
using Microsoft.EntityFrameworkCore;
using Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBRepository.Repositories.Common
{
    /// <summary>
    /// Реализация интерфейса доступа к пользователям
    /// </summary>
    public class AppUserRepository : BaseRepository, IAppUserRepository
    {
        public AppUserRepository(string connectionString, IRepositoryContextFactory contextFactory)
            : base(connectionString, contextFactory) { }

        public async Task<IList<AppUser>> GetAllUsersAsync()
        {
            IList<AppUser> result = null;
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                result = await context.AppUsers.ToListAsync();
            }
            return result;
        }

        public AppUser GetUserById(Guid id)
        {
            AppUser result = null;

            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                result = context.AppUsers
                    .Where(x => x.AppUserId == id).FirstOrDefault();
            }

            return result;
        }

        public AppUser GetUserByLogin(string nickname)
        {
            AppUser result = null;

            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                result = context.AppUsers
                    .Where(x => x.Nickname == nickname).FirstOrDefault();
            }

            return result;
        }

        public AppUser GetUserBySecurityId(Guid securityId)
        {
            AppUser result = null;

            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                result = context.AppUsers
                    .Include(x => x.SecurityUser)
                    .Where(x => x.SecurityUser.SecurityUserId == securityId).FirstOrDefault();
            }

            return result;
        }

        public async Task SaveListAsync(List<AppUser> users)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                // Проверим существуют ли вложенные пользователи системы безопасности
                users.ForEach(user =>
                {
                    var existsSecurity = context.SecurityUsers.Where(x => x.SecurityUserId == user.SecurityUserId).FirstOrDefault();

                    if (existsSecurity != null)
                    {
                        user.SecurityUserId = existsSecurity.SecurityUserId;
                        user.SecurityUser = existsSecurity;
                    }
                });

                context.AppUsers.AddRange(users);
                await context.SaveChangesAsync();
            }
        }

        public int GetUsersCount()
        {
            int result = 0;

            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                result = context.AppUsers.Count();
            }

            return result;
        }
    }
}
