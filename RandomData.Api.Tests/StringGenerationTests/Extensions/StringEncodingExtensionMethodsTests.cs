using FluentAssertions;
using RandomData.Api.StringGeneration.Enums;
using RandomData.Api.StringGeneration.Extensions;
using Xunit;

namespace RandomData.Api.Tests.StringGenerationTests.Extensions
{
    public class StringEncodingExtensionMethodsTests
    {
        [Theory]
        [InlineData(Encoding.Base64, "aAaA", "YUFhQQ==")]
        [InlineData(Encoding.Default, "aAaA", "aAaA")]
        public void EncodeToExtension(Encoding encoding, string input, string output)
        {
            input.EncodeTo(encoding).Should().Be(output);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(" ", "IA==")]
        [InlineData("aAaA", "YUFhQQ==")]
        public void ToBase64Extension(string input, string output)
        {
            input.ToBase64().Should().Be(output);
        }
    }
}