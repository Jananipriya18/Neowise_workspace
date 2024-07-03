using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;


[ApiController]
public class AuthController : ControllerBase
{
    private readonly string _jwtSecret;

    public AuthController(IConfiguration config)
    {
        _jwtSecret = config.GetValue<string>("Jwt:Secret");
    }

    [HttpPost("api/login")]
    public IActionResult Login([FromBody] LoginModel login)
    {
        if (login.Username == "admin" && login.Password == "admin@123" || login.Username == "user" && login.Password == "user@123")
        {
            var token = GenerateJwtToken(login.Username);
            return Ok(new { token });
        }
        else
        {
            return Unauthorized(new { message = "Invalid username or password" });
        }
    }

    private string GenerateJwtToken(string username)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSecret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }),
            Expires = DateTime.UtcNow.AddDays(7), // Token expiration time
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
