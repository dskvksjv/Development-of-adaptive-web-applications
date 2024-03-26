using Microsoft.AspNetCore.Mvc;
using WebApplication2.Interfaces;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public ActionResult<User> Register(User user)
        {
            var newUser = _userService.Register(user);
            if (newUser == null)
                return BadRequest("User with this email already exists.");
            return Ok(newUser);
        }

        [HttpPost("login")]
        public ActionResult<User> Login(LoginRequest loginRequest)
        {
            var user = _userService.Login(loginRequest.Email, loginRequest.Password);
            if (user == null)
                return Unauthorized("Invalid email or password.");
            return Ok(user);
        }
    }
}
