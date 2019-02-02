using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Casino.IServices;
using Casino.Model;

namespace Casino.Controllers
{
    [Authorize]
    [ApiController]
    [Route("casino/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody]User user)
        {
            var token = _userService.Login(user);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]User user)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _userService.Register(user);

            return CreatedAtAction("Get", new { id = user.Id.ToString() }, user);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            var users =  _userService.Get();
            return Ok(users);
        }

        [AllowAnonymous]
        [HttpGet("{id}", Name = "GetbyId")]
        public IActionResult Get(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
