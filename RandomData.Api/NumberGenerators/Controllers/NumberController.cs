using Microsoft.AspNetCore.Mvc;
using RandomData.Api.NumberGenerators.Dto;
using RandomData.Api.NumberGenerators.Generators;
using RandomData.Api.NumberGenerators.Validator;

namespace RandomData.Api.NumberGenerators.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NumberController : ControllerBase
    {
        private readonly NumberGenerator _generator;

        private readonly NumberParametersValidator _validator;

        public NumberController()
        {
            _generator = new NumberGenerator();
            _validator = new NumberParametersValidator();
        }

        [HttpGet]
        public ActionResult<string> GetRandomNumber([FromQuery] NumberParameters parameters)
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
