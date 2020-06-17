﻿using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using RandomData.Api.Services.Random;
using RandomData.Api.Services.Random.ServiceImplementations;
using Xunit;

namespace RandomData.Api.Tests.Services.RandomService
{
    public class RandomServiceHelpersTest
    {
        [Fact]
        public void IsAddRandomWorkingProperly()
        {
            //arrange
            var serviceProvider = new ServiceCollection()
                .AddRandomService()
                .BuildServiceProvider();

            //act/assert
            serviceProvider.GetService<IRandom>().Should().BeOfType<Random>();
        }
    }
}