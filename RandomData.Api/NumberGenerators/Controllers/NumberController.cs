using Microsoft.AspNetCore.Mvc;

namespace RandomData.Api.NumberGenerators.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NumberController : Controller
    {
        [HttpGet]
        public ActionResult<string> GetRandomNumber([FromQuery] string parameters)
        {
            return Ok("");
        }
    }
}
