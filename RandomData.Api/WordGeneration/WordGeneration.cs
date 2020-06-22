using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Caching.Memory;
using RandomData.Api.Exceptions;
using RandomData.Api.Extensions.StringManipulation;
using RandomData.Api.Services.FileReader;
using RandomData.Api.Services.Random;
using RandomData.Api.WordGeneration.Configuration;
using RandomData.Api.WordGeneration.Dto;
using RandomData.Api.WordGeneration.Exceptions;
using RandomData.Api.WordGeneration.Validators;

namespace RandomData.Api.WordGeneration
{
    public class WordGeneration
    {
        private const string WordsDictionaryMemoryCacheKey = "wordsDictionary";
        private readonly IRandomGenerator _randomGenerator;
        private readonly GetWordParametersValidator _validator;
        private readonly ISet<string> _words;

        public WordGeneration(WordsGenerationOptions options,
            IFileReader fileReader, IRandomGenerator randomGenerator, GetWordParametersValidator validator, IMemoryCache memoryCache)
        {
            _randomGenerator = randomGenerator;
            _validator = validator;
            var path = options.WordsDictionaryLocation;
            if (string.IsNullOrEmpty(path)) throw new WordsDictionaryLocationUnspecifiedException();
            var result = memoryCache.TryGetValue(WordsDictionaryMemoryCacheKey, out _words);
            if (!result)
            {
                var content = fileReader.GetFileContent(path);
                try
                {
                    _words = new HashSet<string>(JsonSerializer.Deserialize<string[]>(content));
                    memoryCache.Set(WordsDictionaryMemoryCacheKey, _words, TimeSpan.FromMinutes(5));
                }
                catch (Exception)
                {
                    throw new InvalidWordsDictionaryException();
                }
            }
        }

        public string GenerateRandomString(GetWordParameters parameters)
        {
            var validationResult = _validator.Validate(parameters);
            if (!validationResult.IsValid) throw new InvalidParametersException(validationResult.Errors);
            
            var regex = new Regex($"[^{parameters.AllowedCharacters}]");
            
            var filteredWords = _words
                .Where(x => x.Length >= parameters.MinLength && x.Length <= parameters.MaxLength)
                .Where(x => !regex.IsMatch(x)).ToArray();
            
            if (filteredWords.Length == 0) throw new InvalidWordsDictionaryException();
            return filteredWords[_randomGenerator.Next(0, filteredWords.Length)].FormatTo(parameters.Format)
                .EncodeTo(parameters.Encoding);
        }
    }
}