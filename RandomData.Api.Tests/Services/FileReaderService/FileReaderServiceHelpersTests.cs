using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using RandomData.Api.Services.FileReaderService;
using RandomData.Api.Services.FileReaderService.ServiceImplementations;
using Xunit;

namespace RandomData.Api.Tests.Services.FileReaderService
{
    public class FileReaderServiceHelpersTests
    {
        [Fact]
        public void IsAddFileReaderWorkingProperly()
        {
            //arrange
            var serviceCollection = new ServiceCollection();

            //act
            var serviceProvider = serviceCollection
                .AddFileReaderService()
                .BuildServiceProvider();

            //assert
            serviceProvider.GetService<IFileReaderService>().Should().BeOfType<SystemFileReaderService>();
        }
    }
}