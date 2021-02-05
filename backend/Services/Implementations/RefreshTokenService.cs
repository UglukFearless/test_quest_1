/****************************************************************************
*  Файл: RefreshTokenService.cs
*  Автор: Данилов А.В.
*  Дата создания: 02.02.2021
*  Назначение: Определение класса RefreshTokenService
****************************************************************************/

using backend.Security;
using backend.Services.Interfaces;
using DBRepository.Interfaces;
using Microsoft.Extensions.Logging;
using Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Implementations
{
    /// <summary>
    /// Сервис для работы с токенами обновления
    /// </summary>
    public class RefreshTokenService : IRefreshTokenService
    {
        #region Properties

        private readonly ILogger<RefreshTokenService> _logger;

        private IRefreshTokenRepository _refreshTokenRepository;

        #endregion Properties

        public RefreshTokenService(ILogger<RefreshTokenService> logger, IRefreshTokenRepository refreshTokenRepository)
        {
            _logger = logger;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task DeleteOutdatedAsync()
        {
            await _refreshTokenRepository.DeleteOutdatedAsync();
        }

    }
}
