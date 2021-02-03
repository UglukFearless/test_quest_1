/****************************************************************************
*  Файл: ClaimsDefenitionsService.cs
*  Автор: Данилов А.В.
*  Дата создания: 03.02.2021
*  Назначение: Определение класса ClaimsDefenitionsService
****************************************************************************/

using backend.Services.Interfaces;
using DBRepository.Interfaces;
using Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Implementations
{
    /// <summary>
    /// Реализация интейфейса сервиса разрешений
    /// </summary>
    public class ClaimsDefenitionsService : IClaimsDefenitionsService
    {
        private IClaimsDefenitionsRepository _сlaimsDefenitionsRepository;

        public ClaimsDefenitionsService(IClaimsDefenitionsRepository сlaimsDefenitionsRepository)
        {
            _сlaimsDefenitionsRepository = сlaimsDefenitionsRepository;
        }

        public async Task SaveListAsync(IList<ClaimsDefenition> claims)
        {
            await _сlaimsDefenitionsRepository.SaveListAsync(claims);
        }
    }
}
