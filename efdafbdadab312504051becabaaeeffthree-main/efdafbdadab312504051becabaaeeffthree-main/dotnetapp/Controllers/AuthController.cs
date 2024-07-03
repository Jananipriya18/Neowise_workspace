using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;

// [Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost("api/customerLogin")]
    public IActionResult Login([FromBody] CustomerLogin login)
    {
        if (login.Username == "Neha" && login.Password == "NehaHere#1" && login.Email == "neha@gmail.com" && login.PhoneNumber == "9876543210" && login.TwoFactorEnabledPassCode == "1234" )
        {
            return Ok(new { message = "Customer Login successful" });
        }
        else
        {
            return Unauthorized(new { message = "Invalid Username or Password or Email or PhoneNumber or TwoFactorEnabledPassCode" });
        }
    }
}

