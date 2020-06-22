using System;
using FluentAssertions;
using Newtonsoft.Json;
using RandomData.Api.Services.Random.ServiceImplementations;
using RandomData.Api.StringGeneration;
using RandomData.Api.StringGeneration.Configuration;
using RandomData.Api.StringGeneration.Dto;
using RandomData.Api.StringGeneration.Enums;
using RandomData.Api.StringGeneration.Exceptions;
using RandomData.Api.StringGeneration.ServiceImplementations;
using RandomData.Api.StringGeneration.Validators;
using RandomData.Api.Tests.Services.FileReader;
using Xunit;

namespace RandomData.Api.Tests.StringGenerationTests.ServiceImplementations
{
    public class WordsStringGenerationServiceTests
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
            var service = new WordsStringGenerationService(GetFakeOptions(), fakeFile, new RandomGenerator(), new GetStringParametersValidator());

            //act
            var result = service.GenerateRandomString(new GetStringParameters
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
            var service = new WordsStringGenerationService(GetFakeOptions(), fakeFile, new RandomGenerator(), new GetStringParametersValidator());

            //act
            var result = service.GenerateRandomString(new GetStringParameters
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
            Action a = () => new WordsStringGenerationService(GetFakeOptions(), fakeFile, new RandomGenerator(), new GetStringParametersValidator());

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
            var fakeOptions = new StringGenerationServiceOptions
            {
                WordsDictionaryLocation = path
            };
            Action constructWordsStringGenerationService = ()
                => new WordsStringGenerationService(fakeOptions, fakeFile, new RandomGenerator(), new GetStringParametersValidator());

            //act/assert
            constructWordsStringGenerationService.Should().ThrowExactly<WordsDictionaryLocationUnspecifiedException>();
        }

        private StringGenerationServiceOptions GetFakeOptions()
        {
            return new StringGenerationServiceOptions
                {WordsDictionaryLocation = "fakefile.json"};
        }

        [Fact]
        public void CorrectOrderOfFormattingAndEncoding()
        {
            //arrange
            const string expectedOutput = "aGVsbG9Xb3JsZA==";
            var fakeFile = new FakeFileReader("[\"Hello World\"]");
            var service = new WordsStringGenerationService(GetFakeOptions(), fakeFile, new RandomGenerator(), new GetStringParametersValidator());

            //act
            var result = service.GenerateRandomString(new GetStringParameters
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
            var service = new WordsStringGenerationService(GetFakeOptions(), fakeFile, new RandomGenerator(), new GetStringParametersValidator());

            //act
            var result = service.GenerateRandomString(new GetStringParameters
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
            var service = new WordsStringGenerationService(GetFakeOptions(), fakeFile, new RandomGenerator(), new GetStringParametersValidator());

            //act
            var result = service.GenerateRandomString(new GetStringParameters
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
            var service = new WordsStringGenerationService(GetFakeOptions(), fakeFile, new RandomGenerator(), new GetStringParametersValidator());

            //act/assert
            service.Invoking(x => x.GenerateRandomString(new GetStringParameters
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
            var service = new WordsStringGenerationService(GetFakeOptions(), fakeFile, new RandomGenerator(), new GetStringParametersValidator());

            //act/assert
            service.Invoking(x => x.GenerateRandomString(new GetStringParameters
                {
                    MinLength = 1,
                    MaxLength = 1
                }))
                .Should().ThrowExactly<InvalidWordsDictionaryException>();
        }
    }
}