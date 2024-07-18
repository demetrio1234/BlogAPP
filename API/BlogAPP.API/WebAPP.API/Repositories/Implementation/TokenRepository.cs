using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPP.API.Repositories.Interface;

namespace WebAPP.API.Repositories.Implementation
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;
        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string CreateJwtToken(IdentityUser user, IList<string> roles)
        {
            if (user != null && !string.IsNullOrEmpty(user.Email))
            {
                List<Claim> claims = [
                    new Claim(ClaimTypes.Email, user.Email),
                ];

                if (roles != null)
                    claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                //JWT Security Token Parameters
                SymmetricSecurityKey? key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(s: configuration["Jwt:Key"]));
                SigningCredentials? credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                JwtSecurityToken token = new(
                    issuer: configuration["Jwt:Issuer"],
                    audience: configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: credentials
                    );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            return string.Empty;
        }
    }
}
