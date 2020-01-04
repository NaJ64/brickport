using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BrickPort.Services.Queries;
using BrickPort.Web.Utilities;

namespace BrickPort.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerQueries _playerQueries;

        public PlayersController(IPlayerQueries playerQueries)
        {
            _playerQueries = playerQueries;
        }
        
        [HttpGet, ActionName("Get")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Player>>> GetAsync()
        {
            try
            {
                var players = await _playerQueries.GetAsync();
                return players.ToOk();
            }
            catch(Exception ex)
            {
                return ex.ToInternalServerError();
            }
        }
        
        [HttpGet("{id:guid}"), ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Player>> GetAsync(string id)
        {
            try
            {
                var player = await _playerQueries.GetAsync(id);
                return player.ToOk();
            }
            catch(Exception ex)
            {
                if (ex is KeyNotFoundException)
                    return ex.ToNotFound();
                if (ex is FormatException || 
                    ex is ArgumentException || 
                    ex is ArgumentNullException || 
                    ex is ArgumentOutOfRangeException)
                    return ex.ToBadRequest();
                return ex.ToInternalServerError();
            }
        }

    }
}