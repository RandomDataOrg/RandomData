using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using RandomData.Api.Services.FileReaderService;
using RandomData.Api.Services.StringServices.Enums;
using RandomData.Api.Services.StringServices.Exceptions;
using RandomData.Api.Services.StringServices.Extensions;

namespace RandomData.Api.Services.StringServices.ServiceImplementations
{
    public class WordsStringGenerationService : IStringGenerationService
    {
        private readonly Random _random = new Random();
        private readonly IEnumerable<string> _words;

        public WordsStringGenerationService(StringGenerationServiceHelpers.StringGenerationServiceOptions options,
            IFileReaderService fileReaderService)
        {
            var path = options.WordsDictionaryLocation;
            if (string.IsNullOrEmpty(path)) throw new InvalidWordsDictionaryException();
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

        public string GenerateRandomString(int minLength = 1, int maxLength = int.MaxValue,
            string allowedCharacters = IStringGenerationService.DefaultAllowedCharacters,
            Format format = Format.Default,
            Encoding encoding = Encoding.None)
        {
            var filteredWords = _words
                .Where(x => x.Length >= minLength && x.Length <= maxLength)
                .Where(x => x.ToCharArray().SequenceEqual(x.ToCharArray().Where(allowedCharacters.Contains)))
                .ToArray();
            if (filteredWords.Length == 0) throw new InvalidWordsDictionaryException();
            return filteredWords[_random.Next(0, filteredWords.Length)].FormatTo(format).EncodeTo(encoding);
        }

        public string GenerateRandomString(int length,
            string allowedCharacters = IStringGenerationService.DefaultAllowedCharacters,
            Format format = Format.Default, Encoding encoding = Encoding.None)
        {
            var filteredWords = _words
                .Where(x => x.Length == length)
                .Where(x => x.ToCharArray().SequenceEqual(x.ToCharArray().Where(allowedCharacters.Contains)))
                .ToArray();
            if (filteredWords.Length == 0) throw new InvalidWordsDictionaryException();
            return filteredWords[_random.Next(0, filteredWords.Length)].FormatTo(format).EncodeTo(encoding);
        }
    }
}