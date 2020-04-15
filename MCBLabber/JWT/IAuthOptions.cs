using Microsoft.IdentityModel.Tokens;

namespace MCBLabber.JWT
{
    public interface IAuthOptions
    {
        string Issuer { get; } // издатель токена
        string Audience { get; } // потребитель токена
        int Lifetime { get; } // время жизни токена в минутах
        public SymmetricSecurityKey GetSymmetricSecurityKey(); //ключ
    }
}
