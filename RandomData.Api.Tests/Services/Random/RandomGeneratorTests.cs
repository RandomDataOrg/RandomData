using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using RandomData.Api.Services.Random;
using Xunit;

namespace RandomData.Api.Tests.Services.Random
{
    public class RandomGeneratorTests
    {
        [Fact]
        public void IsAddRandomWorkingProperly()
        {
            //arrange
            var serviceProvider = new ServiceCollection()
                .AddRandomService()
                .BuildServiceProvider();
            
            //act
            var service = serviceProvider.GetService<IRandomGenerator>();

            //assert
            service.Should().BeOfType<Api.Services.Random.ServiceImplementations.RandomGenerator>();
        }
    }
}