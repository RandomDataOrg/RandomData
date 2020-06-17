using FluentAssertions;
using RandomData.Api.Services.RandomService.ServiceImplementations;
using RandomData.Api.StringGeneration.Dto;
using RandomData.Api.StringGeneration.Enums;
using RandomData.Api.StringGeneration.Exceptions;
using RandomData.Api.StringGeneration.ServiceImplementations;
using RandomData.Api.Tests.Services.RandomService;
using Xunit;

namespace RandomData.Api.Tests.StringGenerationTests.ServiceImplementations
{
    public class RandomStringGenerationServiceTests
    {
        [Theory]
        [InlineData(Format.Default, "Hello World")]
        [InlineData(Format.Camel, "helloWorld")]
        [InlineData(Format.Kebab, "hello-world")]
        [InlineData(Format.Lower, "hello world")]
        [InlineData(Format.Pascal, "HelloWorld")]
        [InlineData(Format.Snake, "hello_world")]
        [InlineData(Format.Upper, "HELLO WORLD")]
        public void FormatPropertyTest(Format format, string expectedOutput)
        {
            //arrange
            var random = new FakeRandom(new[] {11, 0, 1, 2, 2, 3, 4, 5, 3, 6, 2, 7});
            var service = new RandomStringGenerationService(random);

            //act
            var result = service.GenerateRandomString(new GetRandomStringParameters
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
        public void EncodingPropertyTest(Encoding encoding, string expectedOutput)
        {
            //arrange
            var random = new FakeRandom(new[] {11, 0, 1, 2, 2, 3, 4, 5, 3, 6, 2, 7});
            var service = new RandomStringGenerationService(random);

            //act
            var result = service.GenerateRandomString(new GetRandomStringParameters
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
        public void CorrectOrderOfFormattingAndEncodingTest()
        {
            //arrange
            const string expectedOutput = "aGVsbG9Xb3JsZA==";
            var random = new FakeRandom(new[] {11, 0, 1, 2, 2, 3, 4, 5, 3, 6, 2, 7});
            var service = new RandomStringGenerationService(random);

            //act
            var result = service.GenerateRandomString(new GetRandomStringParameters
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
        public void LengthPropertyTest()
        {
            //arrange
            var random = new Random();
            var service = new RandomStringGenerationService(random);

            //act
            var result = service.GenerateRandomString(new GetRandomStringParameters
            {
                MinLength = 5,
                MaxLength = 5
            });

            //assert
            result.Length.Should().Be(5);
        }

        [Fact]
        public void MinAndMaxPropertyTest()
        {
            //arrange
            var random = new Random();
            var service = new RandomStringGenerationService(random);

            //act
            var result = service.GenerateRandomString(new GetRandomStringParameters
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
            var random = new Random();
            var service = new RandomStringGenerationService(random);

            //act/assert
            service.Invoking(x => x.GenerateRandomString(new GetRandomStringParameters
                {
                    MinLength = -1
                }))
                .Should().ThrowExactly<InvalidParametersException>();
        }
    }
}