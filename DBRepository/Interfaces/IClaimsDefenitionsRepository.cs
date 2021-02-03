/****************************************************************************
*  Файл: IClaimsDefenitionsRepository.cs
*  Автор: Данилов А.В.
*  Дата создания: 01.02.2021
*  Назначение: Определение интерфейса IClaimsDefenitionsRepository
****************************************************************************/

using Models.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBRepository.Interfaces
{
    /// <summary>
    /// Интерфейс доступа к разрешениям ролей
    /// </summary>
    public interface IClaimsDefenitionsRepository
    {
        /// <summary>
        /// Сохранить список разрешений
        /// </summary>
        /// <param name="claimsDefenitions">Список разрешений</param>
        /// <returns></returns>
        Task SaveListAsync(IList<ClaimsDefenition> claimsDefenitions);
    }
}
