using FluentAssertions;
using RandomData.Api.Services.StringServices.Enums;
using RandomData.Api.Services.StringServices.Extensions;
using Xunit;

namespace RandomData.Api.Tests.Services.StringServices
{
    public class StringEncoderExtensionMethodsTests
    {
        [Theory]
        [InlineData(Encoding.Base64, "aAaA", "YUFhQQ==")]
        [InlineData(Encoding.None, "aAaA", "aAaA")]
        public void EncodeToExtensionTests(Encoding encoding, string input, string output)
        {
            input.EncodeTo(encoding).Should().Be(output);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(" ", "IA==")]
        [InlineData("aAaA", "YUFhQQ==")]
        public void ToBase64ExtensionTests(string input, string output)
        {
            input.ToBase64().Should().Be(output);
        }
    }
}