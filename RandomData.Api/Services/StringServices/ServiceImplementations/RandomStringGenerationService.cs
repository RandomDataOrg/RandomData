using RandomData.Api.Services.RandomService;
using RandomData.Api.Services.StringServices.Dto;
using RandomData.Api.Services.StringServices.Enums;
using RandomData.Api.Services.StringServices.Exceptions;
using RandomData.Api.Services.StringServices.Extensions;

namespace RandomData.Api.Services.StringServices.ServiceImplementations
{
    public class RandomStringGenerationService : IStringGenerationService
    {
        private readonly IRandom _random;
        private readonly StringGenerationServiceDtoValidator _validator = new StringGenerationServiceDtoValidator();

        public RandomStringGenerationService(IRandom random)
        {
            _random = random;
        }

        public string GenerateRandomString(GetRandomStringParameters parameters)
        {
            var validationResult = _validator.Validate(parameters);
            if (!validationResult.IsValid)
            {
                throw new InvalidParametersException(validationResult.Errors);
            }

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