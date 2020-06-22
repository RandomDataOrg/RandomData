using Microsoft.AspNetCore.Mvc;
using RandomData.Api.NumberGenerators.Dtos;
using RandomData.Api.NumberGenerators.Generators;
using RandomData.Api.NumberGenerators.Validators;

namespace RandomData.Api.NumberGenerators.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NumberController : ControllerBase
    {
        private readonly INumberGenerator _generator;

        private readonly INumberParametersValidator _validator;

        public NumberController(INumberGenerator generator, INumberParametersValidator validator)
        {
            _generator = generator;
            _validator = validator;
        }

        [HttpGet]
        public ActionResult<int> GetRandomNumber([FromQuery] NumberParameters parameters)
        {
            var validationResult = _validator.Validate(parameters);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            return Ok(_generator.GetRandom(parameters));
        }
    }
}
