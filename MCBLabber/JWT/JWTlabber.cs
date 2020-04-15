using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MCBLabber.JWT
{
    public class JWTlabber
    {
        public JwtSecurityToken JWT { get; }
        public string EncodedJWT { get => new JwtSecurityTokenHandler().WriteToken(JWT); }

        public JWTlabber(IAuthOptions authOptions, ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            JWT = new JwtSecurityToken(
                issuer: authOptions.Issuer,
                audience: authOptions.Audience,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(authOptions.Lifetime)),
                signingCredentials: new SigningCredentials(authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        }
    }
}
