using FluentAssertions;
using RandomData.Api.Services.RandomService.ServiceImplementations;
using RandomData.Api.Services.StringServices.Enums;
using RandomData.Api.Services.StringServices.ServiceImplementations;
using Xunit;

namespace RandomData.Api.Tests.Services.StringServices.ServiceImplementations
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
            var service = new RandomStringGenerationService(new FakeRandom(new[] {0, 1, 2, 2, 3, 4, 5, 3, 6, 2, 7}));
            service.GenerateRandomString(allowedCharacters: "Helo Wrd", length: 11, format: format).Should()
                .Be(expectedOutput);
        }

        [Theory]
        [InlineData(Encoding.None, "Hello World")]
        [InlineData(Encoding.Base64, "SGVsbG8gV29ybGQ=")]
        public void EncodingPropertyTest(Encoding encoding, string expectedOutput)
        {
            var service = new RandomStringGenerationService(new FakeRandom(new[] {0, 1, 2, 2, 3, 4, 5, 3, 6, 2, 7}));
            service.GenerateRandomString(allowedCharacters: "Helo Wrd", length: 11, encoding: encoding).Should()
                .Be(expectedOutput);
        }

        [Fact]
        public void CorrectOrderOfFormattingAndEncodingTest()
        {
            var service = new RandomStringGenerationService(new FakeRandom(new[] {0, 1, 2, 2, 3, 4, 5, 3, 6, 2, 7}));
            service.GenerateRandomString(allowedCharacters: "Helo Wrd", length: 11, format: Format.Camel,
                    encoding: Encoding.Base64)
                .Should()
                .Be("aGVsbG9Xb3JsZA=="); //helloWorld => Base64
        }

        [Fact]
        public void LengthPropertyTest()
        {
            new RandomStringGenerationService(new Random()).GenerateRandomString(length: 5).Length.Should().Be(5);
        }
    }
}