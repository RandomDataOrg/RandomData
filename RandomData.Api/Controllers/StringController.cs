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
        /// <param name="length">Length of desired string; If equals -1 (default value) length will be random</param>
        /// <param name="minLength">Minimum length of created string (ignored if length is set); Default value is 1</param>
        /// <param name="maxLength">Maximum length of created string (ignored if length is set); Default value is 100</param>
        /// <param name="allowedCharacters">Characters from which string will be built; By default all available ASCII chars</param>
        /// <param name="format">Format of returned string</param>
        /// <param name="encoding">Encoding format of returned string</param>
        /// <returns></returns>
        [Route("random")]
        [Route("")]
        public IActionResult GetRandomString(int length = -1, int minLength = 1, int maxLength = 100,
            string allowedCharacters = IStringGenerationService.DefaultAllowedCharacters,
            Format format = Format.Default,
            Encoding encoding = Encoding.None)
        {
            var stringGenerationService = _serviceResolver(GenerationTypes.Random);
            return Ok(length == -1
                ? stringGenerationService.GenerateRandomString(new StringGenerationServiceDto(minLength, maxLength, allowedCharacters, format,
                    encoding))
                : stringGenerationService.GenerateRandomString(new StringGenerationServiceDto(length, allowedCharacters, format, encoding)));
        }

        /// <summary>
        ///     Returns random word; Requires StringGenerationOptions:WordsGenerationEnabled config option set to true
        /// </summary>
        /// <param name="length">Length of desired string; If equals -1 (default value) length will be random</param>
        /// <param name="minLength">Minimum length of created string (ignored if length is set); Default value is 1</param>
        /// <param name="maxLength">Maximum length of created string (ignored if length is set); Default value is 100</param>
        /// <param name="allowedCharacters">Characters from which string will be built; By default all available ASCII chars</param>
        /// <param name="format">Format of returned string</param>
        /// <param name="encoding">Encoding format of returned string</param>
        /// <returns></returns>
        [Route("word")]
        public IActionResult GetRandomWord(int length = -1, int minLength = 1, int maxLength = 100,
            string allowedCharacters = IStringGenerationService.DefaultAllowedCharacters,
            Format format = Format.Default, Encoding encoding = Encoding.None)
        {
            var stringGenerationService = _serviceResolver(GenerationTypes.Words);
            return Ok(length == -1
                ? stringGenerationService.GenerateRandomString(new StringGenerationServiceDto(minLength, maxLength, allowedCharacters, format,
                    encoding))
                : stringGenerationService.GenerateRandomString(new StringGenerationServiceDto(length, allowedCharacters, format, encoding)));
        }
    }
}