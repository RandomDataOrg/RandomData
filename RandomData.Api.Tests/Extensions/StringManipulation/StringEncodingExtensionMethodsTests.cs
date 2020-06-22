using FluentAssertions;
using RandomData.Api.Extensions.StringManipulation;
using RandomData.Api.Extensions.StringManipulation.Enums;
using Xunit;

namespace RandomData.Api.Tests.Extensions.StringManipulation
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
        [InlineData((string)null, (string)null)]
        [InlineData("", "")]
        [InlineData(" ", "IA==")]
        [InlineData("aAaA", "YUFhQQ==")]
        public void ToBase64Extension(string input, string output)
        {
            input.ToBase64().Should().Be(output);
        }
    }
}