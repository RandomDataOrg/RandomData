using Microsoft.AspNetCore.Mvc;
using RandomData.Api.StringGeneration.Dto;
using RandomData.Api.StringGeneration.ServiceImplementations;
using RandomData.Api.StringGeneration.Validations;

namespace RandomData.Api.StringGeneration.Controllers
{
    [ApiController]
    [Route("/string")]
    public class WordsStringController : Controller
    {
        private readonly WordsStringGenerationService _stringGenerationService;
        private readonly StringGenerationDtoValidator _validator;

        public WordsStringController(WordsStringGenerationService stringGenerationService,
            StringGenerationDtoValidator validator)
        {
            _stringGenerationService = stringGenerationService;
            _validator = validator;
        }

        /// <summary>
        ///     Returns random word
        /// </summary>
        /// <returns></returns>
        [HttpGet("word")]
        public ActionResult<string> GetRandomWord([FromQuery] GetRandomStringParameters parameters)
        {
            var validationResult = _validator.Validate(parameters);
            if (validationResult.IsValid)
                return Ok(_stringGenerationService.GenerateRandomString(parameters));
            return BadRequest(validationResult.Errors);
        }
    }
}