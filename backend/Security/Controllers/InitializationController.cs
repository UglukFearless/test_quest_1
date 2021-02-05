/****************************************************************************
*  Файл: InitializationController.cs
*  Автор: Данилов А.В.
*  Дата создания: 02.02.2021
*  Назначение: Определение класса InitializationController
****************************************************************************/

using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Common;
using Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Security.Controllers
{
    /// <summary>
    /// Контроллер инициализации дефолтных данных
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class InitializationController : Controller
    {
        #region Properties

        private readonly ILogger<InitializationController> _logger;

        private IUserService _userService;
        private IRoleService _roleService;
        private IClaimsDefenitionsService _claimsDefenitionsService;
        private ISecurityUserService _securityUserService;

        #endregion Properties

        public InitializationController(ILogger<InitializationController> logger
                                      , IUserService userService
                                      , IRoleService roleService
                                      , IClaimsDefenitionsService claimsDefenitionsService
                                      , ISecurityUserService securityUserService
            )
        {
            _logger = logger;
            _userService = userService;
            _roleService = roleService;
            _claimsDefenitionsService = claimsDefenitionsService;
            _securityUserService = securityUserService;
        }

        /// <summary>
        /// Метод инициализирует стартовых пользователей и их разрешения
        /// </summary>
        /// <param name="key">ключ безопасности</param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("init")]
        [HttpPost]
        public async Task<IActionResult> InitData([FromBody] Guid key)
        {
            var sid = new Guid("473a7e6f-8b73-455f-a4ee-96292ab89a9a");
            if (!(sid == key))
            {
                return BadRequest("Invalid key");
            }
            else if (_userService.CheckUserExists())
            {
                // Проверим, вдруг пользователи в системе уже заведены
                return BadRequest("Users already created.");
            }

            #region Создание базовых ролей

            var adminRole = new Role
            {
                RoleId = Guid.NewGuid(),
                Code = "MASTER_ADMIN",
                Name = "Владельцы",
                ConcurrencyStamp = Guid.NewGuid(),
            };
            var userRole = new Role
            {
                RoleId = Guid.NewGuid(),
                Code = "SIMPLE_USER",
                Name = "Пользователи",
                ConcurrencyStamp = Guid.NewGuid(),
            };
            var baseRoles = new List<Role>
            {
                adminRole,
                userRole,
            };

            await _roleService.SaveListAsync(baseRoles);

            #endregion Создание базовых ролей

            #region Создание базовых разрешений

            var adminClaimsDefenitions = new List<ClaimsDefenition> {
                new ClaimsDefenition
                {
                    ClaimsDefenitionId = Guid.NewGuid(),
                    RoleId = adminRole.RoleId,
                    Type = CustomClaimTypes.Permission,
                    Value = Permissions.Users.Create,
                },
                new ClaimsDefenition
                {
                    ClaimsDefenitionId = Guid.NewGuid(),
                    RoleId = adminRole.RoleId,
                    Type = CustomClaimTypes.Permission,
                    Value = Permissions.Users.Delete,
                },
                new ClaimsDefenition
                {
                    ClaimsDefenitionId = Guid.NewGuid(),
                    RoleId = adminRole.RoleId,
                    Type = CustomClaimTypes.Permission,
                    Value = Permissions.Users.Edit,
                },
                new ClaimsDefenition
                {
                    ClaimsDefenitionId = Guid.NewGuid(),
                    RoleId = adminRole.RoleId,
                    Type = CustomClaimTypes.Permission,
                    Value = Permissions.Users.Read,
                },
                new ClaimsDefenition
                {
                    ClaimsDefenitionId = Guid.NewGuid(),
                    RoleId = adminRole.RoleId,
                    Type = CustomClaimTypes.Permission,
                    Value = Permissions.Users.ReadDeep,
                },
                new ClaimsDefenition
                {
                    ClaimsDefenitionId = Guid.NewGuid(),
                    RoleId = adminRole.RoleId,
                    Type = CustomClaimTypes.Permission,
                    Value = Permissions.Users.EditSelf,
                },
            };

            var userClaimsDefenitions = new List<ClaimsDefenition> {
                new ClaimsDefenition
                {
                    ClaimsDefenitionId = Guid.NewGuid(),
                    RoleId = userRole.RoleId,
                    Type = CustomClaimTypes.Permission,
                    Value = Permissions.Users.Read,
                },
                new ClaimsDefenition
                {
                    ClaimsDefenitionId = Guid.NewGuid(),
                    RoleId = userRole.RoleId,
                    Type = CustomClaimTypes.Permission,
                    Value = Permissions.Users.EditSelf,
                },
            };

            var allClaimsDefenitions = adminClaimsDefenitions.Concat(userClaimsDefenitions);

            await _claimsDefenitionsService.SaveListAsync(allClaimsDefenitions.ToList());

            #endregion Создание базовых разрешений

            #region Создание базовых пользователей системы безопасности

            var masterAdmin = new SecurityUser
            {
                SecurityUserId = Guid.NewGuid(),
                Login = "Admin",
                Password = BCrypt.Net.BCrypt.HashPassword("Admin"),
                Roles = new List<Role>
                    {
                        adminRole
                    },
            };

            var baseUser = new SecurityUser
            {
                SecurityUserId = Guid.NewGuid(),
                Login = "User",
                Password = BCrypt.Net.BCrypt.HashPassword("User"),
                Roles = new List<Role>
                    {
                        userRole
                    },
            };
            var baseUsers = new List<SecurityUser>
            {
                masterAdmin,
                baseUser,
            };

            await _securityUserService.SaveListAsync(baseUsers);

            #endregion Создание базовых пользователей системы безопасности

            #region Создание базовых пользователей приложения

            var masterAdminUsr = new AppUser
            {
                AppUserId = Guid.NewGuid(),
                SecurityUserId = masterAdmin.SecurityUserId,
                Nickname = "MainAdmin",
                FirstName = "Админ",
                SecondName = "Админ",
                MiddleName = "Админ",
                Registrated = DateTime.Now,
                Birthday = DateTime.Now.AddYears(-31),
            };

            var baseUserUsr = new AppUser
            {
                AppUserId = Guid.NewGuid(),
                SecurityUserId = baseUser.SecurityUserId,
                Nickname = "FirstUser",
                FirstName = "Пользователь",
                SecondName = "Пользователь",
                MiddleName = "Пользователь",
                Registrated = DateTime.Now,
                Birthday = DateTime.Now.AddYears(-31),
            };

            var baseUsersUsr = new List<AppUser>
            {
                masterAdminUsr,
                baseUserUsr,
            };

            await _userService.SaveListAsync(baseUsersUsr);

            #endregion Создание базовых пользователей приложения

            return Ok("Default users are inicialized");
        }
    }
}
