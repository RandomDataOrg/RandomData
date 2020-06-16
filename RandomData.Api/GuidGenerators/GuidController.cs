using Microsoft.AspNetCore.Mvc;

namespace RandomData.Api.GuidGenerators
{
	[ApiController]
	[Route("[controller]")]
	public class GuidController : Controller
	{
		private readonly IGuidGenerator _generator;

		public GuidController(IGuidGenerator generator)
		{
			_generator = generator;
		}

		[HttpGet]
		[Route("")]
		public ActionResult<string> GetRandomGuid([FromQuery] GetRandomGuidParameters parameters)
		{
			return Ok(_generator.GetRandom(parameters));
		}
	}
}
