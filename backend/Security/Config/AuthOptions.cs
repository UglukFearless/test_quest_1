/****************************************************************************
*  Файл: AuthOptions.cs
*  Автор: Данилов А.В.
*  Дата создания: 04.02.2021
*  Назначение: Определение класса AuthOptions
****************************************************************************/

using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace backend.Security
{
    /// <summary>
    /// Класс просто выступает в роли хранителя конфигураций для системы безопасности,
    /// Позже имеет смысл вынести в конфигурационный файл
    /// </summary>
    public class AuthOptions
    {
        public const string ISSUER = "MyTestServer"; // издатель токена
        public const string AUDIENCE = "MyTestClient"; // потребитель токена
        const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
        public const int LIFETIME = 5; // время жизни токена - 5 минут
        public const int REFRESH_LIFETIME = 60; // время жизни токена обновления - 60 минут
        
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
