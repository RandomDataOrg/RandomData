using Microsoft.AspNetCore.Mvc;

namespace RandomData.Api.NumberGenerators.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NumberController : Controller
    {
        [System.Web.Http.HttpGet]
        public ActionResult<string> GetRandomNumber([FromQuery] string parameters)
        {
            return Ok("");
        }
    }
}
