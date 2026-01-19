using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using sakuraShusi.DTO;
using sakuraShusi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace sakuraShusi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(SakuraSushiContext _context) : ControllerBase
    {
        [HttpPost("SignIn")]
        [AllowAnonymous]
        public IActionResult SignIn(SignInDTO user)
        {   
            var passwordHash = HashPassword(user.Password);

            var existingUser = _context.Users
                .FirstOrDefault(u => u.Username == user.Username && u.PasswordHash == passwordHash);

            if (existingUser == null)
            {
                return NotFound(new { message = "Incorrect username or password" });
            }

            var expiresAt = DateTime.UtcNow.AddHours(1);
            var token = GenerateJwtToken(existingUser, expiresAt);

            return Ok(new
            {
                token,
                expiresAt = expiresAt.ToString("o")
            });
        }


        [HttpGet("Me")]
        [Authorize]
        public IActionResult Me()
        {
            var Email = User.Identity?.Name;
            var user = _context.Users.FirstOrDefault(u => u.Email == Email);

            if (user == null)
            {
                return Unauthorized(new { message = "User not found" });
            }

            return Ok(new
            {
                username = user.Username,
                fullName = user.FullName,
                email = user.Email,
                phoneNumber = user.PhoneNumber,
                role = user.Role
            });
        }


        private string HashPassword(string password)
        {
            var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        private string GenerateJwtToken(User user, DateTime expiresAt)
        {
            var keyBytes = Encoding.UTF8.GetBytes(JwtKey.jwtString);
            var securityKey = new SymmetricSecurityKey(keyBytes);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("FullName", user.FullName),
                new Claim("UserId", user.Id.ToString())
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
