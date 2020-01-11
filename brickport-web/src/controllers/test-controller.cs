using System;
using System.Threading.Tasks;
using BrickPort.Services.Commands;
using BrickPort.Web.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrickPort.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IRunTestHandler _runTestHandler;

        public TestController(IRunTestHandler runTestHandler) => _runTestHandler = runTestHandler;

        [HttpPost, ActionName("Post")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> PostAsync([FromBody]RunTestCommand command)
        {
            try 
            {
                var testResult = await _runTestHandler.HandleAsync(command);
                if (string.IsNullOrEmpty(testResult))
                    throw new ApplicationException("Invalid test result");
                return Ok(testResult);
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