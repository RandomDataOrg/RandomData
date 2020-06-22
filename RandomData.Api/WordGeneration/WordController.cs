using Microsoft.AspNetCore.Mvc;
using RandomData.Api.WordGeneration.Dto;
using RandomData.Api.WordGeneration.Validators;

namespace RandomData.Api.WordGeneration
{
    [ApiController] 
    [Route("/word")]
    public class WordController : ControllerBase
    {
        private readonly WordGeneration _wordGeneration;
        private readonly GetWordParametersValidator _validator;

        public WordController(GetWordParametersValidator validator, WordGeneration wordGeneration)
        {
            _validator = validator;
            _wordGeneration = wordGeneration;
        }

        /// <summary>
        ///     Returns random word
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="401">Bad Request</response>
        [Produces("text/plain")]
        [HttpGet("")]
        public ActionResult<string> GetRandomWord([FromQuery] GetWordParameters parameters)
        {
            var validationResult = _validator.Validate(parameters);
            if (validationResult.IsValid)
                return Ok(_wordGeneration.GenerateRandomString(parameters));
            return BadRequest(validationResult.Errors);
        }
    }
}