using System.IO;
using System.Text;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RandomData.Api.Services.FileReader;
using RandomData.Api.Services.Random;
using RandomData.Api.StringGeneration;
using RandomData.Api.StringGeneration.Configuration;
using RandomData.Api.StringGeneration.ServiceImplementations;
using RandomData.Api.Tests.Services.FileReader;
using Xunit;

namespace RandomData.Api.Tests.StringGenerationTests
{
    public class StringGenerationServiceHelpersTests
    {
        [Fact]
        public void IsAddStringGenerationServicesWorkingProperly()
        {
            //arrange
            var serviceCollection = new ServiceCollection();
            var expectedOptions = new StringGenerationServiceOptions
            {
                WordsDictionaryLocation = "words.json"
            };
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonStream(
                    "{\"StringGenerationOptions\" : {\"WordsDictionaryLocation\" : \"words.json\"}}".ToStream())
                .Build();

            //act
            var serviceProvider = serviceCollection
                .AddMemoryCache()
                .AddSingleton<IFileReader>(new FakeFileReader("[\"Hamburger\"]"))
                .AddRandomService()
                .AddStringGenerationServices()
                .AddScoped(_ => configuration)
                .BuildServiceProvider();

            //assert
            serviceProvider
                .GetService<WordsStringGenerationService>().Should().NotBeNull();
            serviceProvider
                .GetService<RandomStringGenerationService>().Should().NotBeNull();
            serviceProvider.GetService<StringGenerationServiceOptions>()
                .Should().BeEquivalentTo(expectedOptions);
        }
    }

    internal static class HelpingExtensions
    {
        internal static Stream ToStream(this string s)
        {
            return new MemoryStream(Encoding.Default.GetBytes(s));
        }
    }
}