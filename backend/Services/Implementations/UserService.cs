/****************************************************************************
*  Файл: UserService.cs
*  Автор: Данилов А.В.
*  Дата создания: 02.02.2021
*  Назначение: Определение класса UserService
****************************************************************************/

using backend.Services.Interfaces;
using DBRepository.Interfaces.Common;
using Microsoft.Extensions.Logging;
using Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Implementations
{
    /// <summary>
    ///  Сервис для работы с пользователями системы
    /// </summary>
    public class UserService : IUserService
    {

        #region Properties

        private readonly ILogger<UserService> _logger;

        private IAppUserRepository _userRepository;

        #endregion Properties

        public UserService(ILogger<UserService> logger, IAppUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public bool CheckUserExists()
        {
            return _userRepository.GetUsersCount() > 0;
        }

        public async Task SaveListAsync(List<AppUser> users)
        {
            await _userRepository.SaveListAsync(users);
        }
    }
}
