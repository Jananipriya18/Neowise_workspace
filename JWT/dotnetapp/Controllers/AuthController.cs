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

    [HttpPost("api/register")]
    public IActionResult Register([FromBody] RegisterModel register)
    {
        // Check if the passwords match
        if (register.Password != register.ConfirmPassword)
        {
            return BadRequest(new { message = "Passwords do not match" });
        }

        return Ok(new { message = $"User '{register.Username}' registered successfully" });
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
