using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MCBLabber.JWT
{
    public class AuthOptions : IAuthOptions
    {
        string IAuthOptions.Issuer => "MCBLabberAPI";
        string IAuthOptions.Audience => "MCBLabberClient";
        int IAuthOptions.Lifetime => 755; //(12ч 35 минут)

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes("u1113p2110p172220p172217"));
        }
    }
}
