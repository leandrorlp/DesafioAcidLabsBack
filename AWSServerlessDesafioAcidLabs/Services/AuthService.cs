using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AWSServerlessDesafioAcidLabs.Services
{
    public class AuthService: IAuthService
    {
        private readonly string _seed;
        private readonly string _expires;

        public AuthService(IConfiguration config)
        {
            _seed = config.GetSection("Jwt").GetSection("seed").Value;
            _expires = config.GetSection("Jwt").GetSection("expirationInMinutes").Value;
        }

        public string GenerateSecurityToken(string user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_seed);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.UserData, user)
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_expires)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}
