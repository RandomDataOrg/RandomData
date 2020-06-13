using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RandomData.Api.Services.StringServices.Enums;
using RandomData.Api.Services.StringServices.Exceptions;
using RandomData.Api.Services.StringServices.ExtensionMethods;

namespace RandomData.Api.Services.StringServices.ServiceImplementations
{
    public class WordsStringGenerationService : IStringGenerationService
    {
        private readonly IEnumerable<string> _words;
        private readonly Random _random = new Random();
        
        public WordsStringGenerationService(IConfiguration configuration)
        {
            var path = configuration.GetValue<string>("StringGenerationOptions:WordsDictionaryLocation");
            if (string.IsNullOrEmpty(path))
            {
                throw new WordsDictionaryLocationUnspecifiedException();
            }
            if (!File.Exists(path))
            {
                File.WriteAllText(path, "[]");
                throw new InvalidWordsDictionaryException();
            }

            var content = File.ReadAllText(path);
            if (string.IsNullOrEmpty(path))
            {
                throw new InvalidWordsDictionaryException();
            }

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
            string allowedCharacters = IStringGenerationService.DefaultAllowedCharacters, Format format = Format.Default,
            Encoding encoding = Encoding.None)
        {
            var filteredWords = _words
                .Where(x => x.Length >= minLength && x.Length <= maxLength)
                .Where(x => x.ToCharArray().SequenceEqual(x.ToCharArray().Where(allowedCharacters.Contains)))
                .ToArray();
            if (filteredWords.Length == 0)
            {
                throw new InvalidWordsDictionaryException();
            }
            return filteredWords[_random.Next(0, filteredWords.Length)].FormatTo(format).EncodeTo(encoding);
        }

        public string GenerateRandomString(int length, string allowedCharacters = IStringGenerationService.DefaultAllowedCharacters,
            Format format = Format.Default, Encoding encoding = Encoding.None)
        {
            var filteredWords = _words
                .Where(x => x.Length == length)
                .Where(x => x.ToCharArray().SequenceEqual(x.ToCharArray().Where(allowedCharacters.Contains)))
                .ToArray();
            if (filteredWords.Length == 0)
            {
                throw new InvalidWordsDictionaryException();
            }
            return filteredWords[_random.Next(0, filteredWords.Length)].FormatTo(format).EncodeTo(encoding);
        }
    }
}