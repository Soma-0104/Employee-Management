using EmployeeManagement.Core.IRepository;
using EmployeeManagement.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagement.Repository
{
    public class TokenRepo : ITokenRepo
    {
        private readonly IConfiguration _config;

        public TokenRepo(IConfiguration config)
        {
            _config = config;
        }

        public string CreateToken(Employees employees)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, employees.EmployeeId.ToString()),
                new Claim(ClaimTypes.Email, employees.Email),
                new Claim(ClaimTypes.Role, employees.Role) 
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"] ?? "Mysecretkey123")
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
