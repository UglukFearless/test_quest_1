/****************************************************************************
*  Файл: SecurityUserService.cs
*  Автор: Данилов А.В.
*  Дата создания: 04.02.2021
*  Назначение: Определение класса SecurityUserService
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
    /// Реализация интерфейса пользователей системы безопасности
    /// </summary>
    public class SecurityUserService : ISecurityUserService
    {

        private ISecurityUserRepository _securityUserRepository;

        public SecurityUserService(ISecurityUserRepository securityUserRepository)
        {
            _securityUserRepository = securityUserRepository;
        }

        public async Task SaveListAsync(List<SecurityUser> users)
        {
            await _securityUserRepository.SaveListAsync(users);
        }
    }
}
