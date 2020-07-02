using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using RandomData.Api.Services.Random;
using RandomData.Api.StringGeneration;
using RandomData.Api.StringGeneration.Validators;
using Xunit;

namespace RandomData.Api.Tests.StringGeneration
{
    public class StringGenerationExtensionsTests
    {
        [Fact]
        public void DoesAddStringGenerationServiceWork()
        {
            //arrange/act
            var serviceProvider = new ServiceCollection()
                .AddRandomService()
                .AddStringGenerationService()
                .BuildServiceProvider();
            
            //assert
            serviceProvider.GetService<Api.StringGeneration.StringGeneration>().Should().NotBeNull();
            serviceProvider.GetService<GetStringParametersValidator>().Should().NotBeNull();
        }
    }
}