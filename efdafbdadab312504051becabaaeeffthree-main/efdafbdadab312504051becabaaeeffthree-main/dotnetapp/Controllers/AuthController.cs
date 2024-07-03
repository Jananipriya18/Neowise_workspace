using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models

// [Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost("api/customerLogin")]
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

