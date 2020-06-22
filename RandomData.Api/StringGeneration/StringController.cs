using Microsoft.AspNetCore.Mvc;
using RandomData.Api.StringGeneration.Dto;
using RandomData.Api.StringGeneration.Validators;

namespace RandomData.Api.StringGeneration
{
    [ApiController]
    [Route("/string")]
    public class StringController : ControllerBase
    {
        private readonly StringGeneration _stringGeneration;
        private readonly GetStringParametersValidator _validator;

        public StringController(StringGeneration stringGeneration,
            GetStringParametersValidator validator)
        {
            _stringGeneration = stringGeneration;
            _validator = validator;
        }

        /// <summary>
        ///     Returns random string
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="401">Bad Request</response>
        [Produces("text/plain")]
        [HttpGet("")]
        public ActionResult<string> GetRandomString([FromQuery] GetStringParameters parameters)
        {
            var validationResult = _validator.Validate(parameters);
            if (validationResult.IsValid)
                return Ok(_stringGeneration.GenerateRandomString(parameters));
            return BadRequest(validationResult.Errors);
        }
    }
}