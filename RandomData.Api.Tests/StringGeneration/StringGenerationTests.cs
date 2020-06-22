using FluentAssertions;
using RandomData.Api.Exceptions;
using RandomData.Api.Extensions.StringManipulation.Enums;
using RandomData.Api.Services.Random.ServiceImplementations;
using RandomData.Api.StringGeneration.Dto;
using RandomData.Api.StringGeneration.Validators;
using RandomData.Api.Tests.Services.Random;
using Xunit;

namespace RandomData.Api.Tests.StringGeneration
{
    public class StringGenerationTests
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
            var random = new FakeRandomGenerator(new[] {11, 0, 1, 2, 2, 3, 4, 5, 3, 6, 2, 7});
            var service = new Api.StringGeneration.StringGeneration(random, new GetStringParametersValidator());

            //act
            var result = service.GenerateRandomString(new GetStringParameters
            {
                MinLength = 11,
                MaxLength = 11,
                AllowedCharacters = "Helo Wrd",
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
            var random = new FakeRandomGenerator(new[] {11, 0, 1, 2, 2, 3, 4, 5, 3, 6, 2, 7});
            var service = new Api.StringGeneration.StringGeneration(random, new GetStringParametersValidator());

            //act
            var result = service.GenerateRandomString(new GetStringParameters
            {
                MinLength = 11,
                MaxLength = 11,
                AllowedCharacters = "Helo Wrd",
                Encoding = encoding
            });

            //assert
            result.Should().Be(expectedOutput);
        }

        [Fact]
        public void CorrectOrderOfFormattingAndEncoding()
        {
            //arrange
            const string expectedOutput = "aGVsbG9Xb3JsZA==";
            var random = new FakeRandomGenerator(new[] {11, 0, 1, 2, 2, 3, 4, 5, 3, 6, 2, 7});
            var service = new Api.StringGeneration.StringGeneration(random, new GetStringParametersValidator());

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
            var random = new RandomGenerator();
            var service = new Api.StringGeneration.StringGeneration(random, new GetStringParametersValidator());

            //act
            var result = service.GenerateRandomString(new GetStringParameters
            {
                MinLength = 5,
                MaxLength = 5
            });

            //assert
            result.Length.Should().Be(5);
        }

        [Fact]
        public void MinAndMaxProperty()
        {
            //arrange
            var random = new RandomGenerator();
            var service = new Api.StringGeneration.StringGeneration(random, new GetStringParametersValidator());

            //act
            var result = service.GenerateRandomString(new GetStringParameters
            {
                MinLength = 1,
                MaxLength = 5
            });

            //assert
            result.Length.Should().BeInRange(1, 5);
        }

        [Fact]
        public void ShouldReturnInvalidParametersException()
        {
            //arrange
            var random = new RandomGenerator();
            var service = new Api.StringGeneration.StringGeneration(random, new GetStringParametersValidator());

            //act/assert
            service.Invoking(x => x.GenerateRandomString(new GetStringParameters
                {
                    MinLength = -1
                }))
                .Should().ThrowExactly<InvalidParametersException>();
        }
    }
}