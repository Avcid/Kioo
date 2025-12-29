using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SimulasiWebaPI_06.DTO;
using SimulasiWebaPI_06.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/cinemaflix/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly CinemaApiContext _db;
    private readonly IConfiguration _config;

    public AuthController(CinemaApiContext db, IConfiguration config)
    {
        _db = db;
        _config = config;
    }

    [HttpPost("login")]
    public IActionResult SignIn(SignInDto user)
    {
        var password = user.Password;

        var existingUser = _db.Users
            .FirstOrDefault(u => u.Email == user.Email && u.Password == password);

        if (existingUser == null)
        {
            return Unauthorized(new { message = "Incorrect email or password" });
        }

        var expiresAt = DateTime.UtcNow.AddHours(1);
        var token = GenerateJwtToken(existingUser, expiresAt);

        return Ok(new
        {
            token,
            expiresAt = expiresAt.ToString("o")

        });
    }

    private string GenerateJwtToken(User user, DateTime expiresAt)
    {
        var keyBytes = Encoding.UTF8.GetBytes(JwtKey.jwtString);
        var securityKey = new SymmetricSecurityKey(keyBytes);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("FullName", user.Fullname)
            };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
