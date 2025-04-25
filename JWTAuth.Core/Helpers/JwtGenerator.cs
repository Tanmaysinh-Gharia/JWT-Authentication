using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JWTAuth.Core.Helpers
{
    public class JwtGenerator
    {
        private readonly IConfiguration _config;

        public JwtGenerator(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateAccessToken(string email, string id, string ipAddress)
        {
            var claims = new[] {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim("ip", ipAddress ?? "unknown")  // attach IP as custom claim
            };

            SigningCredentials creads = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])),
                SecurityAlgorithms.HmacSha256Signature
            );

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(1.0),
                signingCredentials: creads
            );
            var cur = DateTime.Now;
            var after1min = DateTime.Now.AddMinutes(1.0);
            // Serialize the token to a string
            string serielizedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return serielizedToken;
        }
        
        public string GenerateRefreshToken()
        {
            // Generate a random 64-byte string and convert it to Base64
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
    }
}
