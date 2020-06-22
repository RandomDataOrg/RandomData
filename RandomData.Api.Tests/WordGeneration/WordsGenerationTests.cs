using System;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RandomData.Api.Exceptions;
using RandomData.Api.Extensions.StringManipulation.Enums;
using RandomData.Api.Services.Random.ServiceImplementations;
using RandomData.Api.Tests.Services.FileReader;
using RandomData.Api.WordGeneration.Configuration;
using RandomData.Api.WordGeneration.Dto;
using RandomData.Api.WordGeneration.Exceptions;
using RandomData.Api.WordGeneration.Validators;
using Xunit;

namespace RandomData.Api.Tests.WordGeneration
{
    public class WordsGenerationTests
    {
        [Theory]
        [InlineData(Format.Default, "Hello World")]
        [InlineData(Format.Camel, "helloWorld")]
        [InlineData(Format.Kebab, "hello-world")]
        [InlineData(Format.Lower, "hello world")]
        [InlineData(Format.Pascal, "HelloWorld")]
        [InlineData(Format.Snake, "hello_world")]
        [InlineData(Format.Upper, "HELLO WORLD")]
        public void FormatProperty(Format format, string expectedOutput)
        {
            //arrange
            var fakeFile = new FakeFileReader("[\"Hello World\"]");
            var service = new Api.WordGeneration.WordGeneration(GetFakeOptions(), fakeFile, new RandomGenerator(), new GetWordParametersValidator(), new MemoryCache(new MemoryCacheOptions()));

            //act
            var result = service.GenerateRandomString(new GetWordParameters
            {
                Format = format
            });

            //assert
            result.Should().Be(expectedOutput);
        }

        [Theory]
        [InlineData(Encoding.Default, "Hello World")]
        [InlineData(Encoding.Base64, "SGVsbG8gV29ybGQ=")]
        public void EncodingProperty(Encoding encoding, string expectedOutput)
        {
            //arrange
            var fakeFile = new FakeFileReader("[\"Hello World\"]");
            var service = new Api.WordGeneration.WordGeneration(GetFakeOptions(), fakeFile, new RandomGenerator(), new GetWordParametersValidator(), new MemoryCache(new MemoryCacheOptions()));

            //act
            var result = service.GenerateRandomString(new GetWordParameters
            {
                Encoding = encoding
            });

            //assert
            result.Should().Be(expectedOutput);
        }

        [Theory]
        [InlineData((string) null)]
        [InlineData("")]
        [InlineData("{}")]
        [InlineData("[Hamburger]")]
        public void ConstructorShouldReturnInvalidWordsDictionaryExceptionOnInvalidJson(string content)
        {
            //arrange
            var fakeFile = new FakeFileReader(content);
            Action a = () => new Api.WordGeneration.WordGeneration(GetFakeOptions(), fakeFile, new RandomGenerator(), new GetWordParametersValidator(), new MemoryCache(new MemoryCacheOptions()));

            //act/assert
            a.Should().ThrowExactly<InvalidWordsDictionaryException>();
        }

        [Theory]
        [InlineData((string) null)]
        [InlineData("")]
        public void ConstructorShouldReturnWordsDictionaryLocationUnspecifiedExceptionOnInvalidPath(string path)
        {
            //arrange
            var fakeFile = new FakeFileReader("[\"Hamburger\"]");
            var fakeOptions = new WordsGenerationOptions
            {
                WordsDictionaryLocation = path
            };
            Action constructWordsStringGenerationService = ()
                => new Api.WordGeneration.WordGeneration(fakeOptions, fakeFile, new RandomGenerator(), new GetWordParametersValidator(), new MemoryCache(new MemoryCacheOptions()));

            //act/assert
            constructWordsStringGenerationService.Should().ThrowExactly<WordsDictionaryLocationUnspecifiedException>();
        }

        private WordsGenerationOptions GetFakeOptions()
        {
            return new WordsGenerationOptions
                {WordsDictionaryLocation = "fakefile.json"};
        }

        [Fact]
        public void CorrectOrderOfFormattingAndEncoding()
        {
            //arrange
            const string expectedOutput = "aGVsbG9Xb3JsZA==";
            var fakeFile = new FakeFileReader("[\"Hello World\"]");
            var service = new Api.WordGeneration.WordGeneration(GetFakeOptions(), fakeFile, new RandomGenerator(), new GetWordParametersValidator(), new MemoryCache(new MemoryCacheOptions()));

            //act
            var result = service.GenerateRandomString(new GetWordParameters
            {
                MinLength = 11,
                MaxLength = 11,
                AllowedCharacters = "Helo Wrd",
                Format = Format.Camel,
                Encoding = Encoding.Base64
            });

            //assert
            result.Should().Be(expectedOutput);
        }

        [Fact]
        public void LengthProperty()
        {
            //arrange
            var fakeFile = new FakeFileReader(JsonConvert.SerializeObject(new[]
            {
                "Hamburger",
                "Computer",
                "Discord"
            }));
            var service = new Api.WordGeneration.WordGeneration(GetFakeOptions(), fakeFile, new RandomGenerator(), new GetWordParametersValidator(), new MemoryCache(new MemoryCacheOptions()));

            //act
            var result = service.GenerateRandomString(new GetWordParameters
            {
                MinLength = 8,
                MaxLength = 8
            });

            //assert
            result.Should().Be("Computer");
        }

        [Fact]
        public void MinAndMaxProperty()
        {
            //arrange
            var fakeFile = new FakeFileReader(JsonConvert.SerializeObject(new[]
            {
                "Hamburger",
                "Computer",
                "Discord",
                "John",
                "Canary"
            }));
            var service = new Api.WordGeneration.WordGeneration(GetFakeOptions(), fakeFile, new RandomGenerator(), new GetWordParametersValidator(), new MemoryCache(new MemoryCacheOptions()));

            //act
            var result = service.GenerateRandomString(new GetWordParameters
            {
                MinLength = 6,
                MaxLength = 7
            });

            //assert
            result.Should().BeOneOf("Discord", "Canary");
        }

        [Fact]
        public void ShouldReturnInvalidParametersException()
        {
            //arrange
            var fakeFile = new FakeFileReader("[\"Hamburger\"]");
            var service = new Api.WordGeneration.WordGeneration(GetFakeOptions(), fakeFile, new RandomGenerator(), new GetWordParametersValidator(), new MemoryCache(new MemoryCacheOptions()));

            //act/assert
            service.Invoking(x => x.GenerateRandomString(new GetWordParameters
                {
                    MinLength = -1
                }))
                .Should().ThrowExactly<InvalidParametersException>();
        }

        [Fact]
        public void ShouldReturnInvalidWordsDictionaryExceptionOnNoWordsInDictionaryWithGivenLength()
        {
            //arrange
            var fakeFile = new FakeFileReader("[\"Hamburger\"]");
            var service = new Api.WordGeneration.WordGeneration(GetFakeOptions(), fakeFile, new RandomGenerator(), new GetWordParametersValidator(), new MemoryCache(new MemoryCacheOptions()));

            //act/assert
            service.Invoking(x => x.GenerateRandomString(new GetWordParameters
                {
                    MinLength = 1,
                    MaxLength = 1
                }))
                .Should().ThrowExactly<InvalidWordsDictionaryException>();
        }
    }
}