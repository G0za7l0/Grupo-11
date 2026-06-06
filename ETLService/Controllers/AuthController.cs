using Microsoft.AspNetCore.Mvc;
using ETLService.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ETLService.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly string secretKey = "ClaveSecretaXD_2026_SUPER_SEGURA_!$%_JWT";

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // 🔴 SIMULACIÓN (luego conectas BD)
            var user = FakeUser(request);

            if (user == null)
                return Unauthorized("Credenciales inválidas");

            var token = GenerateToken(user);

            return Ok(new { token });
        }

        private User FakeUser(LoginRequest request)
        {
            // Usuario de prueba
            var fake = new User
            {
                Id_User = 1,
                Mail = "admin@test.com",
                Password = BCrypt.Net.BCrypt.HashPassword("1234"),
                Role = "Admin"
            };

            if (request.Mail == fake.Mail &&
                BCrypt.Net.BCrypt.Verify(request.Password, fake.Password))
            {
                return fake;
            }

            return null;
        }

        private string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Mail),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}


