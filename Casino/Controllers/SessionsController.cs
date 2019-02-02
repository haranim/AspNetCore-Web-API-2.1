using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Casino.IServices;
using Casino.Model;
using System.Web;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Casino.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("casino/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private ISessionsService _sessionService;
        private IGamesService _gameservice;
        private IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionsController(ISessionsService sessionService,IGamesService gamesService, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _gameservice = gamesService;
            _userService = userService;
            _sessionService = sessionService;
            _httpContextAccessor = httpContextAccessor;

        }

        [HttpPost]
        public IActionResult CreateSession([FromBody]string gameId)
        {
            string userId = _httpContextAccessor.HttpContext.User.Identity.Name;

            User user = _userService.Get(userId);

            if (user == null)
                return BadRequest("User does not exist");

            Games game = _gameservice.GetGame(gameId);

            if (game == null)
                return BadRequest("Game does not exist");

            var session = _sessionService.CreateSession(game, user);

            if (session == null)
            {
                return BadRequest("Could not create new session");
            }

            return Ok(session);
        }

    }
}
