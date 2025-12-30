using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using SimulasiAkhir.DTOs;
using SimulasiAkhir.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimulasiAkhir.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class AuthController: ControllerBase
    {
        private readonly EsemkaOnePlusContext _db;
        private readonly IConfiguration _config;

        public AuthController(EsemkaOnePlusContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }


        [HttpPost("Auth")]
        public IActionResult Auth(LoginDTO user)
        {
            var pass = user.Password;
            var existingUser = _db.Customers
            .FirstOrDefault(u => u.Email == user.Email && u.Password == pass);

            if (existingUser != null)
            {
                Unauthorized(new { Message = "Incorrect Email or Password" });
            }

            var expiresAt = DateTime.UtcNow.AddHours(1);
            var token = GenerateJwtToken(existingUser, expiresAt);

            return Ok(new
            {
                Token = token,
                expiresAt = expiresAt.ToString("o")
            });

        }
        private string GenerateJwtToken(Customer user, DateTime expiresAt)
        {
            var keyByts = Encoding.UTF8.GetBytes(JwtKey.jwtString);
            var securityKey = new SymmetricSecurityKey(keyByts);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
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
