using DBRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace backend
{
    public class Program
    {
        /// <summary>
        /// Точка входа приложения-сервера
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // получим конфигурацию из json, чтобы создать контекст БД
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json");
            var config = builder.Build();
            // Создадим scope, контекст БД и применим миграции
            using (var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                var factory = service.GetRequiredService<IRepositoryContextFactory>();
                factory.CreateDbContext(config.GetConnectionString("DefaultConnection")).Database.Migrate();
            }

            // запускаем сервер
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
