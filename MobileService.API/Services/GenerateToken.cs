using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MobileService.API.Services
{
    public class GenerateToken : IGenerateToken
    {
        private readonly IConfiguration _configuration;

        public GenerateToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateTokenMethod(string userId)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId), // value: User.Id
            };

            var secretBytes = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]);

            var key = new SymmetricSecurityKey(secretBytes);

            var algorithm = SecurityAlgorithms.HmacSha256;

            var signCredentials = new SigningCredentials(key, algorithm);

            var token = new JwtSecurityToken(
                _configuration["JwtSettings:Issuer"],
                _configuration["JwtSettings:Audenice"],
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(1),
                signCredentials);

            var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);
            
            return tokenJson;
        }
    }
}
