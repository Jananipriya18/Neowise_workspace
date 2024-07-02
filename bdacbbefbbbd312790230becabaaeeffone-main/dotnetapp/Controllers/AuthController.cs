using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models; // Add this line to import the namespace

// [Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost("api/login")]
    public IActionResult Login([FromBody] LoginModel login)
    {
        if (login.Username == "admin" && login.Password == "password")
        {
            return Ok(new { message = "Login successful" });
        }
        else
        {
            return Unauthorized(new { message = "Invalid username or password" });
        }
    }
}

