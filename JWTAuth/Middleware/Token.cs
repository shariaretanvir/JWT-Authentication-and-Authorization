using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuth.Middleware
{
    public class Token : IToken
    {
        private readonly IConfiguration configuration;
        public Token(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string GenerateToken(string para)
        {   
            //authentication claims
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Email, para),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //additional claims
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));

            var authSignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                expires: DateTime.Now.AddHours(Convert.ToDouble(configuration["JWT:Expiry"])),
                claims: claims,
                signingCredentials: new SigningCredentials(authSignKey, SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
