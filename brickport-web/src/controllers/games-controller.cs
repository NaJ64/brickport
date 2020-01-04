using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BrickPort.Services.Commands;
using BrickPort.Services.Queries;
using BrickPort.Web.Utilities;
using Microsoft.AspNetCore.Http;

namespace BrickPort.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameQueries _gameQueries;
        private readonly ICreateGameHandler _createGameHandler;

        public GamesController(IGameQueries gameQueries, ICreateGameHandler createGameHandler)
        {
            _gameQueries = gameQueries;
            _createGameHandler = createGameHandler;
        }
        
        [HttpGet, ActionName("Get")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<GameSummary>>> GetAsync()
        {
            try
            {
                var gameSummaries = await _gameQueries.SummaryAsync();
                return gameSummaries.ToOk();
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
        public async Task<ActionResult<GameSummary>> GetAsync(string id)
        {
            try
            {
                var gameSummary = await _gameQueries.SummaryAsync(id);
                return gameSummary.ToOk();
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

        [HttpPost, ActionName("Post")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> PostAsync([FromBody]CreateGameCommand command)
        {
            try 
            {
                var newGameId = await _createGameHandler.HandleAsync(command);
                return CreatedAtAction("GetById", new { id = newGameId }, newGameId);
            }
            catch(Exception ex)
            {
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