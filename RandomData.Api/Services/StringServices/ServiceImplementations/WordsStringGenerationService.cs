using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using RandomData.Api.Services.FileReaderService;
using RandomData.Api.Services.StringServices.Dto;
using RandomData.Api.Services.StringServices.Enums;
using RandomData.Api.Services.StringServices.Exceptions;
using RandomData.Api.Services.StringServices.Extensions;

namespace RandomData.Api.Services.StringServices.ServiceImplementations
{
    public class WordsStringGenerationService : IStringGenerationService
    {
        private readonly Random _random = new Random();
        private readonly StringGenerationServiceDtoValidator _validator = new StringGenerationServiceDtoValidator();
        private readonly IEnumerable<string> _words;

        public WordsStringGenerationService(StringGenerationServiceHelpers.StringGenerationServiceOptions options,
            IFileReaderService fileReaderService)
        {
            var path = options.WordsDictionaryLocation;
            if (string.IsNullOrEmpty(path)) throw new WordsDictionaryLocationUnspecifiedException();
            var content = fileReaderService.GetFileContent(path);
            try
            {
                _words = JsonSerializer.Deserialize<string[]>(content);
            }
            catch (Exception)
            {
                throw new InvalidWordsDictionaryException();
            }
        }

        public string GenerateRandomString(GetRandomStringParameters parameters)
        {
            var validationResult = _validator.Validate(parameters);
            if (!validationResult.IsValid)
            {
                throw new InvalidParametersException(validationResult.Errors);
            }
            
            var filteredWords = _words
                .Where(x => x.Length >= parameters.MinLength && x.Length <= parameters.MaxLength)
                .Where(x => x.ToCharArray().SequenceEqual(x.ToCharArray().Where(parameters.AllowedCharacters.Contains)))
                .ToArray();
            if (filteredWords.Length == 0) throw new InvalidWordsDictionaryException();
            return filteredWords[_random.Next(0, filteredWords.Length)].FormatTo(parameters.Format).EncodeTo(parameters.Encoding);
        }
    }
}