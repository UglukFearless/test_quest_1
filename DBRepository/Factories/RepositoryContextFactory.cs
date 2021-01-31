/****************************************************************************
*  Файл: RepositoryContextFactory.cs
*  Автор: Данилов А.В.
*  Дата создания: 01.02.2021
*  Назначение: Определение интерфейса RepositoryContextFactory
****************************************************************************/

using DBRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace DBRepository.Factories
{
    /// <summary>
    /// Реализация фабрики создания контекста базы данных
    /// </summary>
    public class RepositoryContextFactory : IRepositoryContextFactory
    {
        /// <summary>
        /// Метод создающий контекст базы данных MsSql
        /// </summary>
        /// <param name="connectionString">Строка подключения к базе данных MsSql</param>
        /// <returns>Контекст базы данных MsSql</returns>
        public RepositoryContext CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new RepositoryContext(optionsBuilder.Options);
        }
    }
}
