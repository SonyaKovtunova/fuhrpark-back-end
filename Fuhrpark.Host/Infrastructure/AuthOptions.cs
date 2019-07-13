using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuhrpark.Host.Infrastructure
{
    public class AuthOptions
    {
        public const string ISSUER = "Anthill.p21"; // издатель токена
        public const string AUDIENCE = "http://anthill-outbrain-mvp.com/"; // потребитель токена
        const string KEY = "r#ryLh?wwJ2;7,7m6[MU&`R";   // ключ для шифрации
        public const int LIFETIME = 1440; // время жизни токена - 1 минута

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }
    }
}
