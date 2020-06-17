using System.IO;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RandomData.Api.Services.FileReader;
using RandomData.Api.Services.Random;
using RandomData.Api.StringGeneration;
using RandomData.Api.StringGeneration.ServiceImplementations;
using RandomData.Api.Tests.Services.FileReaderService;
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
            var expectedOptions = new StringGenerationServiceHelpers.StringGenerationServiceOptions
            {
                WordsDictionaryLocation = "words.json"
            };
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonStream(
                    "{\"StringGenerationOptions\" : {\"WordsDictionaryLocation\" : \"words.json\"}}".ToStream())
                .Build();

            //act
            var serviceProvider = serviceCollection
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
            serviceProvider.GetService<StringGenerationServiceHelpers.StringGenerationServiceOptions>()
                .Should().BeEquivalentTo(expectedOptions);
        }
    }

    internal static class HelpingExtensions
    {
        internal static Stream ToStream(this string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}