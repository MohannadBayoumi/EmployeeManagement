using EmployeeManagementSystem.BL.Interfaces;
using EmployeeManagementSystem.Entities.Enitites;
using EmployeeManagementSystemDBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagementSystem.BL.Implementations
{
    public class AuthorizationBL : IAuthorizationBL
    {
        private readonly TeleDBContext _dbContext;
        private readonly IConfiguration _configuration;
        public AuthorizationBL(TeleDBContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        public string Login(string username, string password)
        {
            string hashedPassword = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
            var user = _dbContext.SystemUsers.Include(p => p.UserRoles).FirstOrDefault(u => u.Username == username && u.Password == hashedPassword);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var roles = (from role in _dbContext.Roles.AsEnumerable()
                        where user.UserRoles.Any(e => e.RoleId == role.Id)
                        select new Claim(ClaimTypes.Role, role.Name)).ToList();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, username)
            };
            claims.AddRange(roles);

            var secretKey = _configuration["JWTConfiguration:SecretKey"];
            if (secretKey == null)
            {
                throw new Exception("Secret key not found");
            }

            var jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JWTConfiguration:TokenValidationInMinutes"])),
                SigningCredentials = new SigningCredentials(jwtKey, SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            
            return handler.WriteToken(token);
        }
    }
}
