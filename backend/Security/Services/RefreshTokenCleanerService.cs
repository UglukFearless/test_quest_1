/****************************************************************************
*  Файл: RefreshTokenCleanerService.cs
*  Автор: Данилов А.В.
*  Дата создания: 04.02.2021
*  Назначение: Определение класса RefreshTokenCleanerService
****************************************************************************/

using backend.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace backend.Security.Services
{
    /// <summary>
    /// Интерфейс сервиса для работы с токенами обновления
    /// </summary>
    public class RefreshTokenCleanerService : IHostedService, IDisposable
    {
        private readonly ILogger<RefreshTokenCleanerService> _logger;
        private Timer _timer;
        private IRefreshTokenService _refreshTokenService;

        /// <summary>
        /// Интервал срабатывания сервиса
        /// </summary>
        private int _timeInterval = 30;

        public RefreshTokenCleanerService(ILogger<RefreshTokenCleanerService> logger, IRefreshTokenService refreshTokenService)
        {
            _logger = logger;
            _refreshTokenService = refreshTokenService;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        // запуск сервиса
        public Task StartAsync(System.Threading.CancellationToken cancellationToken)
        {
            _logger.LogInformation("Refresh Token Cleaner running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromMinutes(this._timeInterval));

            return Task.CompletedTask;
        }

        // остановка сервиса
        public Task StopAsync(System.Threading.CancellationToken cancellationToken)
        {
            _logger.LogInformation("Refresh Token Cleaner Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Основная работа, выполняемая по таймеру - удаление устревших токенов обновления
        /// </summary>
        /// <param name="state"></param>
        private void DoWork(object state)
        {
            _logger.LogInformation("Refresh Token Cleaner Service is srarted working.");
            var task = _refreshTokenService.DeleteOutdatedAsync();
            task.Wait();
        }
    }
}
