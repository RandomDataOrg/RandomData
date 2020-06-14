using System;
using FluentAssertions;
using RandomData.Api.Services.StringServices.Enums;
using RandomData.Api.Services.StringServices.Extensions;
using Xunit;

namespace RandomData.Api.Tests.Services.StringServices
{
    public class StringFormatterExtensionMethodsTests
    {
        [Theory]
        [InlineData(Format.Pascal, "aa AA aa", "AaAaAa")]
        [InlineData(Format.Kebab, "aa AA aa", "aa-aa-aa")]
        [InlineData(Format.Lower, "aa AA aa", "aa aa aa")]
        [InlineData(Format.Upper, "aa AA aa", "AA AA AA")]
        [InlineData(Format.Camel, "aa AA aa", "aaAaAa")]
        [InlineData(Format.Snake, "aa AA aa", "aa_aa_aa")]
        [InlineData(Format.Default, "aa AA aa", "aa AA aa")]
        public void FormatToExtensionTests(Format format, string input, string output)
        {
            input.FormatTo(format).Should().Be(output);
        }
        
        [Theory]
        [InlineData("", "")]
        [InlineData(" ", "")]
        [InlineData("  ", "")]
        [InlineData("AAA", "Aaa")]
        [InlineData("aaaa", "Aaaa")]
        [InlineData("aAaA", "Aaaa")]
        [InlineData("aa AA aa", "AaAaAa")]
        public void ToPascalCaseExtensionTests(string input, string output)
        {
            input.ToPascalCase().Should().Be(output);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(" ", "")]
        [InlineData("  ", "")]
        [InlineData("AAA", "aaa")]
        [InlineData("aaaa", "aaaa")]
        [InlineData("aAaA", "aaaa")]
        [InlineData("aa AA aa", "aaAaAa")]
        public void ToCamelCaseExtensionTests(string input, string output)
        {
            input.ToCamelCase().Should().Be(output);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(" ", "")]
        [InlineData("  ", "")]
        [InlineData("AAA", "aaa")]
        [InlineData("aaaa", "aaaa")]
        [InlineData("aAaA", "aaaa")]
        [InlineData("aa AA aa", "aa_aa_aa")]
        public void ToSnakeCaseExtensionTests(string input, string output)
        {
            input.ToSnakeCase().Should().Be(output);
        }
        
        [Theory]
        [InlineData("", "")]
        [InlineData(" ", "")]
        [InlineData("  ", "")]
        [InlineData("AAA", "aaa")]
        [InlineData("aaaa", "aaaa")]
        [InlineData("aAaA", "aaaa")]
        [InlineData("aa AA aa", "aa-aa-aa")]
        public void ToKebabCaseExtensionTests(string input, string output)
        {
            input.ToKebabCase().Should().Be(output);
        }
    }
}