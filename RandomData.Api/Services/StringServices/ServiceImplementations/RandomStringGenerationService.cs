using System;
using RandomData.Api.Services.StringServices.Enums;
using RandomData.Api.Services.StringServices.ExtensionMethods;

namespace RandomData.Api.Services.StringServices.ServiceImplementations
{
    public class RandomStringGenerationService : IStringGenerationService
    {
        private readonly Random _random = new Random();

        public string GenerateRandomString(int minLength = 1, int maxLength = int.MaxValue,
            string allowedCharacters = IStringGenerationService.DefaultAllowedCharacters, Format format = Format.Default,
            Encoding encoding = Encoding.None)
        {
            var length = _random.Next(minLength, maxLength);
            return GenerateRandomString(length, allowedCharacters, format, encoding);
        }

        public string GenerateRandomString(int length, string allowedCharacters = IStringGenerationService.DefaultAllowedCharacters,
            Format format = Format.Default, Encoding encoding = Encoding.None)
        {
            var stringChars = new char[length];
            
            for (var i = 0; i < length; i++)
            {
                stringChars[i] = allowedCharacters[_random.Next(0, allowedCharacters.Length)];
            }
            
            return new string(stringChars)
                .FormatTo(format)
                .EncodeTo(encoding);
        }
    }
}