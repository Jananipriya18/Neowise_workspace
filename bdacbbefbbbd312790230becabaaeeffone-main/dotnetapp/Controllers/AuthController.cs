using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;

// [Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost("api/register")]
    public IActionResult Register([FromBody] RegistrationModel register)
    {
        if (register.Username == "manager" && register.Password == "Manager@123" && register.Email == "manager@gmail.com")
        {
            return Ok(new { message = "Registration successful" });
        }
        else
        {
            return Unauthorized(new { message = "Invalid username or Password or Email" });
        }
    }
}
