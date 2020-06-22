using Microsoft.AspNetCore.Mvc;
using RandomData.Api.StringGeneration.Dto;
using RandomData.Api.StringGeneration.ServiceImplementations;
using RandomData.Api.StringGeneration.Validators;

namespace RandomData.Api.StringGeneration.Controllers
{
    [ApiController]
    [Route("/string")]
    public class WordsStringController : ControllerBase
    {
        private readonly WordsStringGenerationService _stringGenerationService;
        private readonly GetStringParametersValidator _validator;

        public WordsStringController(WordsStringGenerationService stringGenerationService,
            GetStringParametersValidator validator)
        {
            _stringGenerationService = stringGenerationService;
            _validator = validator;
        }

        /// <summary>
        ///     Returns random word
        /// </summary>
        [HttpGet("word")]
        public ActionResult<string> GetRandomWord([FromQuery] GetStringParameters parameters)
        {
            var validationResult = _validator.Validate(parameters);
            if (validationResult.IsValid)
                return Ok(_stringGenerationService.GenerateRandomString(parameters));
            return BadRequest(validationResult.Errors);
        }
    }
}