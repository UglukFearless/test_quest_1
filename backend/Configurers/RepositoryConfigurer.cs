/****************************************************************************
*  Файл: RepositoryConfigurer.cs
*  Автор: Данилов А.В.
*  Дата создания: 02.02.2021
*  Назначение: Определение класса RepositoryConfigurer
****************************************************************************/

using DBRepository.Factories;
using DBRepository.Interfaces;
using DBRepository.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Configurers
{
    /// <summary>
    /// Класс, предназначенный для выноса болока конфигурации одной группы в отдельную область
    /// </summary>
    public static class RepositoryConfigurer
    {

        /// <summary>
        /// Метод определяет необходимые для работы репозитории и области их видимости
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        public static void ConfigureRepositories(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IRepositoryContextFactory, RepositoryContextFactory>();

            services.AddScoped<ISecurityUserRepository>(
                provider => new SecurityUserRepository(connectionString, provider.GetService<IRepositoryContextFactory>())
                );
            services.AddScoped<IRoleRepository>(
                provider => new RoleRepository(connectionString, provider.GetService<IRepositoryContextFactory>())
                );
            services.AddScoped<IClaimsDefenitionsRepository>(
                provider => new ClaimsDefenitionsRepository(connectionString, provider.GetService<IRepositoryContextFactory>())
                );
            services.AddTransient<IRefreshTokenRepository>(
                provider => new RefreshTokenRepository(connectionString, provider.GetService<IRepositoryContextFactory>())
                );
        }
    }
}
