using Microsoft.AspNetCore.Mvc;

namespace RandomData.Api.DateTimeGenerators
{
    [Route("[controller]")]
    [ApiController]
    public class DateTimeController : ControllerBase
    {
		private readonly DateTimeGenerator _dateTimeGenerator;

		public DateTimeController(DateTimeGenerator dateTimeGenerator)
		{
			_dateTimeGenerator = dateTimeGenerator;
		}

		/// <summary>
		/// Returns random DateTime between min and max dates.
		/// </summary>
		[HttpGet]
		public IActionResult GetRandomDateTime([FromQuery] RandomDateTimeParameters request)
		{
			return Ok(_dateTimeGenerator.Generate(request));
		}
	}
}