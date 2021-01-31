/****************************************************************************
*  Файл: DesignTimeRepositoryContextFactory.cs
*  Автор: Данилов А.В.
*  Дата создания: 01.02.2021
*  Назначение: Определение класса DesignTimeRepositoryContextFactory
****************************************************************************/

using DBRepository;
using DBRepository.Factories;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace backend
{
    /// <summary>
    /// Класс необходимый для работы с миграциями
    /// </summary>
    public class DesignTimeRepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json");

            var config = builder.Build();
            var connectionString = config.GetConnectionString("DefaultConnection");
            var repositoryFactory = new RepositoryContextFactory();

            return repositoryFactory.CreateDbContext(connectionString);
        }
    }
}
