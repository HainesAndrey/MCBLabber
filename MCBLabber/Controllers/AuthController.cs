using MCBLabber.JWT;
using MCBLabber.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MCBLabber.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly LabberDBContext _context;

        public AuthController(LabberDBContext context)
        {
            _context = context;
        }

        [HttpPost("gettoken")]
        public async Task<IActionResult> GetTokenAsync([FromBody]User user)
        {
            var identity = await GetIdentityAsync(user.Name, user.Password);
            if (identity == null)
                return BadRequest(new { errorText = "Некорректные логин или пароль." });

            JWTlabber jwtLabber = new JWTlabber(new AuthOptions(), identity);

            var response = new
            {
                access_token = jwtLabber.EncodedJWT,
                username = identity.Name,
                role = identity.Claims.First(x => x.Type is ClaimsIdentity.DefaultRoleClaimType).Value,
                validTo = jwtLabber.JWT.ValidTo
            };

            return Ok(response);
        }

        private async Task<ClaimsIdentity> GetIdentityAsync(string username, string password)
        {
            User user = await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Name == username && x.Password == password);
            if (user == null)
                return null;

            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Title)
                };

            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}