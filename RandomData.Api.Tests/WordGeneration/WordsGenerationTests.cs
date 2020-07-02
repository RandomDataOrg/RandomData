using System;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RandomData.Api.Exceptions;
using RandomData.Api.Extensions.StringManipulation.Enums;
using RandomData.Api.Services.FileReader;
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
            var fakeFile = new FakeFileReader("Hello World");
            var service = GetWordGenerationService(fakeFile);

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
            var fakeFile = new FakeFileReader("Hello World");
            var service = GetWordGenerationService(fakeFile);

            //act
            var result = service.GenerateRandomString(new GetWordParameters
            {
                Encoding = encoding
            });

            //assert
            result.Should().Be(expectedOutput);
        }
        
        [Fact]
        public void ConstructorShouldReturnInvalidWordsDictionaryExceptionOnInvalidList()
        {
            //arrange
            var fakeFile = new FakeFileReader(null);
            Action a = () => GetWordGenerationService(fakeFile);

            //act/assert
            a.Should().ThrowExactly<InvalidWordsDictionaryException>();
        }

        [Theory]
        [InlineData((string) null)]
        [InlineData("")]
        public void ConstructorShouldReturnWordsDictionaryLocationUnspecifiedExceptionOnInvalidPath(string path)
        {
            //arrange
            var fakeFile = new FakeFileReader("Hamburger");
            var fakeOptions = new WordsGenerationOptions
            {
                WordsDictionaryLocation = path
            };
            Action constructWordsStringGenerationService = ()
                => GetWordGenerationService(fakeFile, fakeOptions);

            //act/assert
            constructWordsStringGenerationService.Should().ThrowExactly<WordsDictionaryLocationUnspecifiedException>();
        }

        [Fact]
        public void CorrectOrderOfFormattingAndEncoding()
        {
            //arrange
            const string expectedOutput = "aGVsbG9Xb3JsZA==";
            var fakeFile = new FakeFileReader("Hello World");
            var service = GetWordGenerationService(fakeFile);

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
            var fakeFile = new FakeFileReader(string.Join(';', new[]
            {
                "Hamburger",
                "Computer",
                "Discord"
            }));
            var service = GetWordGenerationService(fakeFile);

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
            var fakeFile = new FakeFileReader(string.Join(';',new[]
            {
                "Hamburger",
                "Computer",
                "Discord",
                "John",
                "Canary"
            }));
            var service = GetWordGenerationService(fakeFile);

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
            var fakeFile = new FakeFileReader("Hamburger");
            var service = GetWordGenerationService(fakeFile);

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
            var fakeFile = new FakeFileReader("Hamburger");
            var service = GetWordGenerationService(fakeFile);

            //act/assert
            service.Invoking(x => x.GenerateRandomString(new GetWordParameters
                {
                    MinLength = 1,
                    MaxLength = 1
                }))
                .Should().ThrowExactly<InvalidWordsDictionaryException>();
        }

        private static Api.WordGeneration.WordGeneration GetWordGenerationService(IFileReader fileReader, WordsGenerationOptions options = null)
        {
            return new Api.WordGeneration.WordGeneration(options ?? GetFakeOptions(), new RandomGenerator(),
                new GetWordParametersValidator(),fileReader, new MemoryCache(new MemoryCacheOptions()));
        }
        private static WordsGenerationOptions GetFakeOptions()
        {
            return new WordsGenerationOptions
                {WordsDictionaryLocation = "fakefile.txt"};
        }
    }
}