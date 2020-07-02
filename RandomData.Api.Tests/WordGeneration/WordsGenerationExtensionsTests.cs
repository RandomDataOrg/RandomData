using System.IO;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RandomData.Api.Services.FileReader;
using RandomData.Api.Services.Random;
using RandomData.Api.Tests.Services.FileReader;
using RandomData.Api.WordGeneration;
using RandomData.Api.WordGeneration.Configuration;
using Xunit;

namespace RandomData.Api.Tests.WordGeneration
{
    public class WordsGenerationExtensionsTests
    {
        [Fact]
        public void DoesAddWordsGenerationServiceWork()
        {
            //arrange
            var expectedOptions = new WordsGenerationOptions
            {
                WordsDictionaryLocation = "words.json"
            };
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonStream(
                    "{\"StringGenerationOptions\" : {\"WordsDictionaryLocation\" : \"words.json\"}}".ToStream())
                .Build();
            
            //act
            var serviceProvider = new ServiceCollection()
                .AddMemoryCache()
                .AddSingleton<IFileReader>(new FakeFileReader("[\"Hamburger\"]"))
                .AddRandomService()
                .AddWordGenerationService()
                .AddScoped(_ => configuration)
                .BuildServiceProvider();
            
            //assert
            serviceProvider.GetService<Api.WordGeneration.WordGeneration>().Should().NotBeNull();
            serviceProvider.GetService<WordsGenerationOptions>().Should().BeEquivalentTo(expectedOptions);
        }
    }
    
    internal static class HelpingExtensions
    {
        internal static Stream ToStream(this string s)
        {
            return new MemoryStream(System.Text.Encoding.Default.GetBytes(s));
        }
    }
}