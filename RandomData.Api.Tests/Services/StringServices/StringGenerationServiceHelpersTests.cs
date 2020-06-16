using System.IO;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RandomData.Api.Services.FileReaderService;
using RandomData.Api.Services.FileReaderService.ServiceImplementations;
using RandomData.Api.Services.RandomService;
using RandomData.Api.Services.StringServices;
using RandomData.Api.Services.StringServices.Enums;
using RandomData.Api.Services.StringServices.ServiceImplementations;
using Xunit;

namespace RandomData.Api.Tests.Services.StringServices
{
    public class StringGenerationServiceHelpersTests
    {
        [Fact]
        public void IsAddStringGenerationServicesWorkingProperly()
        {
            //arrange
            var serviceCollection = new ServiceCollection();
            var expectedOptions = new StringGenerationServiceHelpers.StringGenerationServiceOptions()
            {
                WordsDictionaryLocation = "words.json"
            };
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonStream("{\"StringGenerationOptions\" : {\"WordsDictionaryLocation\" : \"words.json\"}}".ToStream())
                .Build();

            //act
            var serviceProvider = serviceCollection
                .AddSingleton<IFileReaderService>(new FakeFileReaderService("[\"Hamburger\"]"))
                .AddRandomService()
                .AddStringGenerationServices()
                .AddScoped(_ => configuration)
                .BuildServiceProvider();

            //assert
            var resolver = serviceProvider
                .GetService<StringGenerationServiceHelpers.StringGenerationServiceResolver>();
            resolver.Should().NotBeNull();
            resolver(GenerationTypes.Words).Should().BeOfType<WordsStringGenerationService>();
            resolver(GenerationTypes.Random).Should().BeOfType<RandomStringGenerationService>();
            serviceProvider.GetService<StringGenerationServiceHelpers.StringGenerationServiceOptions>()
                .Should().BeEquivalentTo(expectedOptions);
        }
    }

    internal static class Extensions
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