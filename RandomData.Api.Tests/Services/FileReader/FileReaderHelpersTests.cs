using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using RandomData.Api.Services.FileReader;
using RandomData.Api.Services.FileReader.ServiceImplementations;
using Xunit;

namespace RandomData.Api.Tests.Services.FileReader
{
    public class FileReaderHelpersTests
    {
        [Fact]
        public void IsAddFileReaderRegisteredCorrectly()
        {
            //arrange
            var serviceCollection = new ServiceCollection();

            //act
            var serviceProvider = serviceCollection
                .AddFileReaderService()
                .BuildServiceProvider();

            //assert
            serviceProvider.GetService<IFileReader>().Should().BeOfType<SystemFileReader>();
        }
    }
}