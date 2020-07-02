using FluentAssertions;
using RandomData.Api.Extensions.StringManipulation;
using RandomData.Api.Extensions.StringManipulation.Enums;
using Xunit;

namespace RandomData.Api.Tests.Extensions.StringManipulation
{
    public class StringFormattingExtensionMethodsTests
    {
        [Theory]
        [InlineData(Format.Pascal, "aa AA aa", "AaAaAa")]
        [InlineData(Format.Kebab, "aa AA aa", "aa-aa-aa")]
        [InlineData(Format.Lower, "aa AA aa", "aa aa aa")]
        [InlineData(Format.Upper, "aa AA aa", "AA AA AA")]
        [InlineData(Format.Camel, "aa AA aa", "aaAaAa")]
        [InlineData(Format.Snake, "aa AA aa", "aa_aa_aa")]
        [InlineData(Format.Default, "aa AA aa", "aa AA aa")]
        public void FormatToExtension(Format format, string input, string output)
        {
            input.FormatTo(format).Should().Be(output);
        }

        [Theory]
        [InlineData((string)null, (string)null)]
        [InlineData("", "")]
        [InlineData(" ", "")]
        [InlineData("  ", "")]
        [InlineData("AAA", "Aaa")]
        [InlineData("aaaa", "Aaaa")]
        [InlineData("aAaA", "Aaaa")]
        [InlineData("aa AA aa", "AaAaAa")]
        [InlineData("aa   AA", "AaAa")]
        public void ToPascalCaseExtension(string input, string output)
        {
            input.ToPascalCase().Should().Be(output);
        }

        [Theory]
        [InlineData((string)null, (string)null)]
        [InlineData("", "")]
        [InlineData(" ", "")]
        [InlineData("  ", "")]
        [InlineData("AAA", "aaa")]
        [InlineData("aaaa", "aaaa")]
        [InlineData("aAaA", "aaaa")]
        [InlineData("aa AA aa", "aaAaAa")]
        [InlineData("aa   AA", "aaAa")]
        public void ToCamelCaseExtension(string input, string output)
        {
            input.ToCamelCase().Should().Be(output);
        }

        [Theory]
        [InlineData((string)null, (string)null)]
        [InlineData("", "")]
        [InlineData(" ", "")]
        [InlineData("  ", "")]
        [InlineData("AAA", "aaa")]
        [InlineData("aaaa", "aaaa")]
        [InlineData("aAaA", "aaaa")]
        [InlineData("aa AA aa", "aa_aa_aa")]
        [InlineData("aa   AA", "aa___aa")]
        public void ToSnakeCaseExtension(string input, string output)
        {
            input.ToSnakeCase().Should().Be(output);
        }

        [Theory]
        [InlineData((string)null, (string)null)]
        [InlineData("", "")]
        [InlineData(" ", "")]
        [InlineData("  ", "")]
        [InlineData("AAA", "aaa")]
        [InlineData("aaaa", "aaaa")]
        [InlineData("aAaA", "aaaa")]
        [InlineData("aa AA aa", "aa-aa-aa")]
        [InlineData("aa   AA", "aa---aa")]
        public void ToKebabCaseExtension(string input, string output)
        {
            input.ToKebabCase().Should().Be(output);
        }
    }
}