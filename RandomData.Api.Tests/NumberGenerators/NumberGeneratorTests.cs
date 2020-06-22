using RandomData.Api.NumberGenerators.Dtos;
using RandomData.Api.NumberGenerators.Generators;
using Xunit;

namespace RandomData.Api.Tests.NumberGenerators
{
    public class NumberGeneratorTests
    {
        [Fact]
        public void NumberGenerator_WithDefaultParameters_ReturnsPositiveNumber()
        {
            //arrange
            var generator = new NumberGenerator();
            var parameters = new NumberParameters();

            //act
            var result = generator.GetRandom(parameters);

            //assert
            Assert.True(result >= 0);
        }

        [Fact]
        public void NumberGenerator_WithAllowedDigits_ReturnsNumberWithAllowedDigitsOnly()
        {
            //arrange
            var generator = new NumberGenerator();
            var parameters = new NumberParameters()
            {
                AllowedDigits = "2"
            };

            //act
            var result = generator.GetRandom(parameters).ToString();

            //assert
            Assert.Contains("2", result);
        }

        [Fact]
        public void NumberGenerator_WithMinLength_ReturnsNumberNoShorterThanMin()
        {
            //arrange
            var generator = new NumberGenerator();
            var parameters = new NumberParameters()
            {
                MinLength = 3
            };

            //act
            var result = generator.GetRandom(parameters);

            //assert
            Assert.True(result >= 100);
        }

        [Fact]
        public void NumberGenerator_WithMaxLength_ReturnsNumberNoLongerThanMax()
        {
            //arrange
            var generator = new NumberGenerator();
            var parameters = new NumberParameters()
            {
                MaxLength = 3
            };

            //act
            var result = generator.GetRandom(parameters);

            //assert
            Assert.True(result <= 999);
        }

        [Fact]
        public void NumberGenerator_WithSameMinAndMaxLength_ReturnsNumberWithExactLength()
        {
            //arrange
            var generator = new NumberGenerator();
            var parameters = new NumberParameters()
            {
                MaxLength = 3,
                MinLength = 3
            };

            //act
            var result = generator.GetRandom(parameters);

            //assert
            Assert.True(result >= 100 && result <= 999);
        }
    }
}
