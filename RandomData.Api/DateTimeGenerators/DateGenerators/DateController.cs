using Microsoft.AspNetCore.Mvc;

namespace RandomData.Api.DateTimeGenerators.DateGenerators
{
	[Route("[controller]")]
	[ApiController]
	public class DateController : ControllerBase
	{
		private readonly DateGenerator _dateGenerator;

		public DateController(DateGenerator dateGenerator)
		{
			_dateGenerator = dateGenerator;
		}

		/// <summary>
		/// Returns random date between min and max dates.
		/// </summary>
		[HttpGet]
		public IActionResult GetRandomDateTime([FromQuery] RandomDateParameters request)
		{
			return Ok(_dateGenerator.Generate(request));
		}
	}
}