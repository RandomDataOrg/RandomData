using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using RandomData.Api.Services.FileReader;
using RandomData.Api.Services.Random;
using RandomData.Api.StringGeneration.Configuration;
using RandomData.Api.StringGeneration.Dto;
using RandomData.Api.StringGeneration.Exceptions;
using RandomData.Api.StringGeneration.Extensions;
using RandomData.Api.StringGeneration.Validators;

namespace RandomData.Api.StringGeneration.ServiceImplementations
{
    public class WordsStringGenerationService : IStringGenerationService
    {
        private readonly IRandomGenerator _randomGenerator;
        private readonly GetStringParametersValidator _validator;
        private readonly ISet<string> _words;

        public WordsStringGenerationService(StringGenerationServiceOptions options,
            IFileReader fileReader, IRandomGenerator randomGenerator, GetStringParametersValidator validator)
        {
            _randomGenerator = randomGenerator;
            _validator = validator;
            var path = options.WordsDictionaryLocation;
            if (string.IsNullOrEmpty(path)) throw new WordsDictionaryLocationUnspecifiedException();
            var content = fileReader.GetFileContent(path);
            try
            {
                _words = new HashSet<string>(JsonSerializer.Deserialize<string[]>(content));
            }
            catch (Exception)
            {
                throw new InvalidWordsDictionaryException();
            }
        }

        public string GenerateRandomString(GetStringParameters parameters)
        {
            var validationResult = _validator.Validate(parameters);
            if (!validationResult.IsValid) throw new InvalidParametersException(validationResult.Errors);

            var filteredWords = _words
                .Where(x => x.Length >= parameters.MinLength && x.Length <= parameters.MaxLength)
                .Where(x => x.ToCharArray().SequenceEqual(x.ToCharArray().Where(parameters.AllowedCharacters.Contains)))
                .ToArray();
            if (filteredWords.Length == 0) throw new InvalidWordsDictionaryException();
            return filteredWords[_randomGenerator.Next(0, filteredWords.Length)].FormatTo(parameters.Format)
                .EncodeTo(parameters.Encoding);
        }
    }
}