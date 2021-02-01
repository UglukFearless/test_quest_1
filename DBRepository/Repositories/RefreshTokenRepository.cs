/****************************************************************************
*  Файл: RefreshTokenRepository.cs
*  Автор: Данилов А.В.
*  Дата создания: 02.02.2021
*  Назначение: Определение класса RefreshTokenRepository
****************************************************************************/

using DBRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Security;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DBRepository.Repositories
{
    /// <summary>
    /// реализация интерфейса доступа к токенам обновления
    /// </summary>
    public class RefreshTokenRepository : BaseRepository, IRefreshTokenRepository
    {
        public RefreshTokenRepository(string connectionString, IRepositoryContextFactory contextFactory) 
            : base(connectionString, contextFactory) { }

        public async Task DeleteRefreshToken(string token)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var refreshToken = context.RefreshTokens
                    .Where(x => x.Token == token)
                    .FirstOrDefault();

                if (refreshToken != null)
                {
                    context.RefreshTokens.Remove(refreshToken);
                    await context.SaveChangesAsync();
                }
            }
        }

        public RefreshToken GetByTokenAndLoginAndSession(string token, string login, Guid session)
        {
            RefreshToken result;
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                result = context.RefreshTokens
                    .Include(r => r.SecurityUser)
                    .Where(x => x.Token == token && x.SecurityUser.Login == login && x.Session == session)
                    .FirstOrDefault();
            }
            return result;
        }

        public RefreshToken GetByLoginAndSession(string login, Guid session)
        {
            RefreshToken result;
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                result = context.RefreshTokens.Include(r => r.SecurityUser)
                    .Where(x => x.SecurityUser.Login == login && x.Session == session)
                    .FirstOrDefault();
            }
            return result;
        }

        public async Task SaveRefreshToken(RefreshToken refreshToken)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                if (refreshToken != null)
                {
                    context.RefreshTokens.Add(refreshToken);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteAllToDateRefreshToken(DateTime toDate)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var deleteList = context.RefreshTokens.Where(x => x.TimeOfDeath < toDate);
                context.RefreshTokens.RemoveRange(deleteList);
                await context.SaveChangesAsync();
            }
        }
    }
}
