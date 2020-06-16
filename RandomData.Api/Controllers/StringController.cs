using Microsoft.AspNetCore.Mvc;
using RandomData.Api.Services.StringServices;
using RandomData.Api.Services.StringServices.Dto;
using RandomData.Api.Services.StringServices.Enums;

namespace RandomData.Api.Controllers
{
    [ApiController]
    [Route("/string")]
    public class StringController : Controller
    {
        private readonly StringGenerationServiceHelpers.StringGenerationServiceResolver _serviceResolver;

        public StringController(StringGenerationServiceHelpers.StringGenerationServiceResolver serviceResolver)
        {
            _serviceResolver = serviceResolver;
        }

        /// <summary>
        ///     Returns random string
        /// </summary>
        /// <returns></returns>
        [HttpGet("random")]
        [HttpGet("")]
        public ActionResult<string> GetRandomString([FromQuery]GetRandomStringParameters parameters)
        {
            var stringGenerationService = _serviceResolver(GenerationTypes.Random);
            return Ok(stringGenerationService.GenerateRandomString(parameters));
        }

        /// <summary>
        ///     Returns random word; Requires StringGenerationOptions:WordsGenerationEnabled config option set to true
        /// </summary>
        /// <returns></returns>
        [HttpGet("word")]
        public ActionResult<string> GetRandomWord([FromQuery]GetRandomStringParameters parameters)
        {
            var stringGenerationService = _serviceResolver(GenerationTypes.Words);
            return Ok(stringGenerationService.GenerateRandomString(parameters));
        }
    }
}