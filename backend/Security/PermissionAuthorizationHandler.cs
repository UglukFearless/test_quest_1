/****************************************************************************
*  Файл: PermissionAuthorizationHandler.cs
*  Автор: Данилов А.В.
*  Дата создания: 04.02.2021
*  Назначение: Определение класса PermissionAuthorizationHandler
****************************************************************************/

using DBRepository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Security
{
    /// <summary>
    /// Обработчик, проверяющий наличие разрешений у пользователя осуществляющего запрос
    /// </summary>
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {

        private ISecurityUserRepository _userRepository;

        public PermissionAuthorizationHandler(ISecurityUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Проверяет наличие разрешений у пользователя осуществляющего запрос
        /// </summary>
        /// <param name="context">Контекст запроса</param>
        /// <param name="requirement">Запрос разрешения</param>
        /// <returns></returns>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null)
            {
                return;
            }

            // получим пользователя системы безопасности по его логину (в параметре Name)
            var user = _userRepository.GetUserByLogin(context.User.Identity.Name);

            if (user == null)
            {
                return;
            }

            // Переберём роли и их разрешения в цикле, пока не найдём нужное
            user.Roles.ForEach(role =>
            {
                var permissions = role.ClaimsDefenitions.Where(x => x.Type == CustomClaimTypes.Permission
                                                                 && x.Value == requirement.Permission)
                                                              .Select(s => s.Value);
                if (permissions.Any())
                {
                    context.Succeed(requirement);
                    return;
                }
            });
        }
    }
}
