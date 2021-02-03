/****************************************************************************
*  Файл: RoleService.cs
*  Автор: Данилов А.В.
*  Дата создания: 03.02.2021
*  Назначение: Определение класса RoleService
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
    /// Реализация интейфейса сервиса ролей
    /// </summary>
    public class RoleService : IRoleService
    {
        private IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task SaveListAsync(IList<Role> roles)
        {
            await _roleRepository.SaveListAsync(roles);
        }
    }
}
