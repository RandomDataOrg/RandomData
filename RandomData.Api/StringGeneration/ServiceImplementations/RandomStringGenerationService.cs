using System.Linq;
using RandomData.Api.Services.Random;
using RandomData.Api.StringGeneration.Dto;
using RandomData.Api.StringGeneration.Exceptions;
using RandomData.Api.StringGeneration.Extensions;
using RandomData.Api.StringGeneration.Validators;

namespace RandomData.Api.StringGeneration.ServiceImplementations
{
    public class RandomStringGenerationService : IStringGenerationService
    {
        private readonly IRandomGenerator _randomGenerator;
        private readonly GetStringParametersValidator _validator;

        public RandomStringGenerationService(IRandomGenerator randomGenerator, GetStringParametersValidator validator)
        {
            _randomGenerator = randomGenerator;
            _validator = validator;
        }

        public string GenerateRandomString(GetStringParameters parameters)
        {
            var validationResult = _validator.Validate(parameters);
            if (!validationResult.IsValid) throw new InvalidParametersException(validationResult.Errors);

            var length = _randomGenerator.Next(parameters.MinLength, parameters.MaxLength);

            var result = new string(Enumerable.Range(0, length).Select(x=>GetRandomChar(parameters.AllowedCharacters)).ToArray());

            return result
                .FormatTo(parameters.Format)
                .EncodeTo(parameters.Encoding);
        }

        private char GetRandomChar(string allowedChars)
        {
            return allowedChars[_randomGenerator.Next(0, allowedChars.Length)];
        }
    }
}