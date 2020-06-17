using Microsoft.AspNetCore.Mvc;
using RandomData.Api.StringGeneration.Dto;
using RandomData.Api.StringGeneration.ServiceImplementations;
using RandomData.Api.StringGeneration.Validations;

namespace RandomData.Api.StringGeneration.Controllers
{
    [ApiController]
    [Route("/string")]
    public class RandomStringController : Controller
    {
        private readonly RandomStringGenerationService _stringGenerationService;
        private readonly StringGenerationDtoValidator _validator;

        public RandomStringController(RandomStringGenerationService stringGenerationService,
            StringGenerationDtoValidator validator)
        {
            _stringGenerationService = stringGenerationService;
            _validator = validator;
        }

        /// <summary>
        ///     Returns random string
        /// </summary>
        /// <returns></returns>
        [HttpGet("random")]
        [HttpGet("")]
        public ActionResult<string> GetRandomString([FromQuery] GetRandomStringParameters parameters)
        {
            var validationResult = _validator.Validate(parameters);
            if (validationResult.IsValid)
                return Ok(_stringGenerationService.GenerateRandomString(parameters));
            return BadRequest(validationResult.Errors);
        }
    }
}