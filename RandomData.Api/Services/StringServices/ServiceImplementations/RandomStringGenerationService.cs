using RandomData.Api.Services.RandomService;
using RandomData.Api.Services.StringServices.Dto;
using RandomData.Api.Services.StringServices.Enums;
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

        public string GenerateRandomString(StringGenerationServiceDto dto)
        {
            var validationResult = _validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                //TODO Throw exception
                throw new System.NotImplementedException();
            }

            var length = _random.Next(dto.MinLength, dto.MaxLength);
            
            var stringChars = new char[length];
            
            for (var i = 0; i < length; i++)
                stringChars[i] = dto.AllowedCharacters[_random.Next(0, dto.AllowedCharacters.Length)];

            return new string(stringChars)
                .FormatTo(dto.Format)
                .EncodeTo(dto.Encoding);
        }
    }
}