using Microsoft.AspNetCore.Mvc;

namespace RandomData.Api.GuidGenerators
{
	[ApiController]
	[Route("[controller]")]
	public class GuidController : Controller
	{
		private readonly IGuidGenerator _generator;
		private readonly IGetRandomGuidParametersValidator _validator;

		public GuidController(IGuidGenerator generator, IGetRandomGuidParametersValidator validator)
		{
			_generator = generator;
			_validator = validator;
		}

		[HttpGet]
		[Route("")]
		public ActionResult<string> GetRandomGuid([FromQuery] GetRandomGuidParameters parameters)
		{
			var validationResult = _validator.Validate(parameters);
			if (!validationResult.IsValid)
				return BadRequest(validationResult.Errors);
			return Ok(_generator.GetRandom(parameters));
		}
	}
}
