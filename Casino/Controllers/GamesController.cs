using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Casino.IServices;
using Casino.Model;
using System;

namespace Casino.Controllers
{
    [Authorize]
    [Route("casino/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private IGamesService _gamesService;

        public GamesController(IGamesService gamesService)
        {
            _gamesService = gamesService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetGames(int? skip, int? take)
        {
            try
            {
                var games = _gamesService.GetGames( skip ?? 0, take ?? 10);
                if (games == null)
                {
                    return NotFound("Games not found");
                }

                return Ok(games);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

            
        }


        [AllowAnonymous]
        [HttpGet("collection")]
        public IActionResult GetGamesCollection()
        {
            var collection = _gamesService.GetGamesCollection();

            if (collection == null)
            {
                return NotFound("Games collection not found");
            }
            return Ok(collection);
        }

        
        
    }
}