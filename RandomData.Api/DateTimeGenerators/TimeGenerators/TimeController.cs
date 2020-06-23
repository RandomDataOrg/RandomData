using Microsoft.AspNetCore.Mvc;

namespace RandomData.Api.DateTimeGenerators.TimeGenerators
{
	[Route("[controller]")]
	[ApiController]
	public class TimeController : ControllerBase
	{
		private readonly TimeGenerator _timeGenerator;

		public TimeController(TimeGenerator timeGenerator)
		{
			_timeGenerator = timeGenerator;
		}

		/// <summary>
		/// Returns random time between min and max dates.
		/// </summary>
		[HttpGet]
		public IActionResult GetRandomDateTime([FromQuery] RandomTimeParameters request)
		{
			return Ok(_timeGenerator.Generate(request));
		}
	}
}