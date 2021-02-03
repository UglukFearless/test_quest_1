/****************************************************************************
*  Файл: ClaimsDefenitionsRepository.cs
*  Автор: Данилов А.В.
*  Дата создания: 01.02.2021
*  Назначение: Определение класса ClaimsDefenitionsRepository
****************************************************************************/

using DBRepository.Interfaces;
using Models.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBRepository.Repositories
{
    /// <summary>
    /// Реаоизация интерфейса доступа к разрешениям ролей
    /// </summary>
    public class ClaimsDefenitionsRepository : BaseRepository, IClaimsDefenitionsRepository
    {
        public ClaimsDefenitionsRepository(string connectionString, IRepositoryContextFactory contextFactory)
            : base(connectionString, contextFactory) { }

        public async Task SaveListAsync(IList<ClaimsDefenition> claimsDefenitions)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                context.ClaimsDefenitions.AddRange(claimsDefenitions);
                await context.SaveChangesAsync();
            };
        }
    }
}
