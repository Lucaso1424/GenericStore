using GenericStore.Domain.Entities;
using GenericStore.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Core.Application.Services
{
    public class UtilsService
    {
        private readonly IConfiguration _configuration;
        public UtilsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string EncryptSHA256(string input)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }

            return builder.ToString();
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("scope", "api.read")
            };

            switch((RoleId)user.RoleId)
            {
                case RoleId.Admin:
                    claims.Add(new Claim(ClaimTypes.Role, RoleId.Admin.ToString()));
                    break;
                case RoleId.Employee:
                    claims.Add(new Claim(ClaimTypes.Role, RoleId.Employee.ToString()));
                    break;
                case RoleId.Customer:
                    claims.Add(new Claim(ClaimTypes.Role, RoleId.Customer.ToString()));
                    break;
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var jwtConfig = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:ExpireMinutes"]!)),
                signingCredentials: credential,
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"]
            );
            return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
        }
    }
}