using RandomData.Api.Services.Random;
using RandomData.Api.StringGeneration.Dto;
using RandomData.Api.StringGeneration.Exceptions;
using RandomData.Api.StringGeneration.Extensions;
using RandomData.Api.StringGeneration.Validators;

namespace RandomData.Api.StringGeneration.ServiceImplementations
{
    public class RandomStringGenerationService : IStringGenerationService
    {
        private readonly IRandom _random;
        private readonly GetStringParametersValidator _validator = new GetStringParametersValidator();

        public RandomStringGenerationService(IRandom random)
        {
            _random = random;
        }

        public string GenerateRandomString(GetStringParameters parameters)
        {
            var validationResult = _validator.Validate(parameters);
            if (!validationResult.IsValid) throw new InvalidParametersException(validationResult.Errors);

            var length = _random.Next(parameters.MinLength, parameters.MaxLength);

            var stringChars = new char[length];

            for (var i = 0; i < length; i++)
                stringChars[i] = parameters.AllowedCharacters[_random.Next(0, parameters.AllowedCharacters.Length)];

            return new string(stringChars)
                .FormatTo(parameters.Format)
                .EncodeTo(parameters.Encoding);
        }
    }
}