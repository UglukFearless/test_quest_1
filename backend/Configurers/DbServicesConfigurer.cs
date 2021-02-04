/****************************************************************************
*  Файл: DbServicesConfigurer.cs
*  Автор: Данилов А.В.
*  Дата создания: 04.02.2021
*  Назначение: Определение класса DbServicesConfigurer
****************************************************************************/

using backend.Services.Implementations;
using backend.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Configurers
{
    /// <summary>
    /// Класс, предназначенный для выноса болока конфигурации сервисов работы с сущностями БД в отдельную область
    /// </summary>
    public class DbServicesConfigurer
    {
        /// <summary>
        /// Метод определяет необходимые для работы репозитории и области их видимости
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        public static void ConfigureAdd(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IClaimsDefenitionsService, ClaimsDefenitionsService>();
            services.AddScoped<ISecurityUserService, SecurityUserService>();
            services.AddTransient<IRefreshTokenService, RefreshTokenService>();
        }
    }
}
