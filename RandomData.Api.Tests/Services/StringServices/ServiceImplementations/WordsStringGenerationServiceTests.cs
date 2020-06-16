﻿using System;
using FluentAssertions;
using Newtonsoft.Json;
using RandomData.Api.Services.FileReaderService.ServiceImplementations;
using RandomData.Api.Services.StringServices;
using RandomData.Api.Services.StringServices.Enums;
using RandomData.Api.Services.StringServices.Exceptions;
using RandomData.Api.Services.StringServices.ServiceImplementations;
using Xunit;

namespace RandomData.Api.Tests.Services.StringServices.ServiceImplementations
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
        public void FormatPropertyTest(Format format, string expectedOutput)
        {
            //arrange
            var fakeFile = new FakeFileReaderService("[\"Hello World\"]");
            var service = new WordsStringGenerationService(GetFakeOptions(), fakeFile);
            
            //act
            var result = service.GenerateRandomString(format: format);
            
            //assert
            result.Should().Be(expectedOutput);
        }

        [Theory]
        [InlineData(Encoding.None, "Hello World")]
        [InlineData(Encoding.Base64, "SGVsbG8gV29ybGQ=")]
        public void EncodingPropertyTest(Encoding encoding, string expectedOutput)
        {
            //arrange
            var fakeFile = new FakeFileReaderService("[\"Hello World\"]");
            var service = new WordsStringGenerationService(GetFakeOptions(), fakeFile);
            
            //act
            var result = service.GenerateRandomString(encoding: encoding);
            
            //assert
            result.Should().Be(expectedOutput);
        }

        [Fact]
        public void CorrectOrderOfFormattingAndEncodingTest()
        {
            //arrange
            const string expectedOutput = "aGVsbG9Xb3JsZA==";
            var fakeFile = new FakeFileReaderService("[\"Hello World\"]");
            var service = new WordsStringGenerationService(GetFakeOptions(), fakeFile);
            
            //act
            var result = service.GenerateRandomString(allowedCharacters: "Helo Wrd", length: 11, format: Format.Camel,
                encoding: Encoding.Base64);
            
            //assert
            result.Should().Be(expectedOutput);
        }

        [Fact]
        public void LengthPropertyTest()
        {
            //arrange
            var fakeFile = new FakeFileReaderService(JsonConvert.SerializeObject(new []
            {
                "Hamburger",
                "Computer",
                "Discord"
            }));
            var service = new WordsStringGenerationService(GetFakeOptions(), fakeFile);
            
            //act
            var result = service.GenerateRandomString(length: 8);
            
            //assert
            result.Should().Be("Computer");
        }

        [Fact]
        public void MinAndMaxPropertyTest()
        {
            //arrange
            var fakeFile = new FakeFileReaderService(JsonConvert.SerializeObject(new []
            {
                "Hamburger",
                "Computer",
                "Discord",
                "John",
                "Canary"
            }));
            var service = new WordsStringGenerationService(GetFakeOptions(), fakeFile);
            
            //act
            var result = service.GenerateRandomString(6, 7);
            
            //assert
            result.Should().BeOneOf("Discord", "Canary");
        }

        [Theory]
        [InlineData((string)null)]
        [InlineData("")]
        [InlineData("{}")]
        [InlineData("[Hamburger]")]
        public void ConstructorShouldReturnInvalidWordsDictionaryExceptionOnInvalidJson(string content)
        {
            //arrange
            var fakeFile = new FakeFileReaderService(content);
            Action a = () => new WordsStringGenerationService(GetFakeOptions(), fakeFile);
            
            //act/assert
            a.Should().ThrowExactly<InvalidWordsDictionaryException>();
        }

        [Theory]
        [InlineData((string)null)]
        [InlineData("")]
        public void ConstructorShouldReturnWordsDictionaryLocationUnspecifiedExceptionOnInvalidPath(string path)
        {
            //arrange
            var fakeFile = new FakeFileReaderService("[\"Hamburger\"]");
            var fakeOptions = new StringGenerationServiceHelpers.StringGenerationServiceOptions()
            {
                WordsDictionaryLocation = path
            };
            Action a = () => new WordsStringGenerationService(fakeOptions, fakeFile);
            
            //act/assert
            a.Should().ThrowExactly<WordsDictionaryLocationUnspecifiedException>();
        }
        
        [Fact]
        public void ShouldReturnInvalidWordsDictionaryExceptionOnNoWordsInDictionaryWithGivenLength()
        {
            //arrange
            var fakeFile = new FakeFileReaderService("[\"Hamburger\"]");
            var service = new WordsStringGenerationService(GetFakeOptions(), fakeFile);
            
            //act/assert
            service.Invoking(x=>x.GenerateRandomString(length:1))
                .Should().ThrowExactly<InvalidWordsDictionaryException>();
        }

        private StringGenerationServiceHelpers.StringGenerationServiceOptions GetFakeOptions() => 
            new StringGenerationServiceHelpers.StringGenerationServiceOptions 
                {WordsDictionaryLocation = "fakefile.json"};
    }
}