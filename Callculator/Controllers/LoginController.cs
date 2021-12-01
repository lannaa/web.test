using System.Linq;
using Callculator.DB;
using Callculator.Models;
using Microsoft.AspNetCore.Mvc;

namespace Callculator.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        [HttpPost("remind")]
        public IActionResult Remind(string email)
        {
            email = email?.ToLower() ?? string.Empty;

            if (!email.IsValidEmail())
            {
                return Conflict("Invalid email.");
            }

            if (!Users.Emails.Contains(email))
            {
                return Conflict("No user was found.");
            }

            return Json($"Email with instructions was sent to {email}");
        }

        [HttpPost("validate")]
        public IActionResult Validate([FromBody] UserDto dto)
        {
            if (Users.IsValid(dto.Login, dto.Password))
            {
                return Ok();
            }

            // *********************************************
            if (Users.Names.Any(x => x.ToLower() == dto.Login.ToLower()))
            {
                return NotFound("Incorrect password!");
            }

            if (Users.Passwords.Any(x => x == dto.Password))
            {
                return NotFound("Incorrect user name!");
            }

            if (Users.Names.All(x => x.ToLower() != dto.Login.ToLower()))
            {
                return NotFound($"'{dto.Login}' user doesn't exist!");
            }
            // *********************************************

            return NotFound("Incorrect user name or password!");
        }
    }
}
