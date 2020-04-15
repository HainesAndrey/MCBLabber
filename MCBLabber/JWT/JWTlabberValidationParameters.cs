using Microsoft.IdentityModel.Tokens;
using System;

namespace MCBLabber.JWT
{
    public class JWTlabberValidationParameters : TokenValidationParameters
    {
        public JWTlabberValidationParameters(IAuthOptions authOptions) : base()
        {
            ValidateIssuer = true;
            ValidateAudience = true;
            ValidateLifetime = true;
            ValidateIssuerSigningKey = true;
            ValidIssuer = authOptions.Issuer;
            ValidAudience = authOptions.Audience;
            IssuerSigningKey = authOptions.GetSymmetricSecurityKey();
            ClockSkew = TimeSpan.FromSeconds(0);
        }
    }
}
