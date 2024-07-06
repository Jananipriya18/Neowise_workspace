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
        if (IsValidUser(login.Username, login.Password))
        {
            var token = GenerateJwtToken(login.Username);
            return Ok(new { token });
        }
        else
        {
            return Unauthorized(new { message = "Invalid username or password" });
        }
    }

    [HttpPost("api/register")]
    public IActionResult Register([FromBody] RegisterModel register)
    {
        // Check if the passwords match
        if (register.Password != register.ConfirmPassword)
        {
            return BadRequest(new { message = "Passwords do not match" });
        }

        // For demo purposes, assume registration is successful
        // Normally, you would hash the password and save to database
        // Here, we'll just return a success message with the username
        return Ok(new { message = $"User '{register.Username}' registered successfully" });
    }

    private bool IsValidUser(string username, string password)
    {
        // Replace with actual authentication logic, e.g., database lookup
        return (username == "admin" && password == "admin@123") || (username == "user" && password == "user@123");
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
