using RandomData.Api.NumberGenerators.Dto;
using RandomData.Api.NumberGenerators.Validator;
using Xunit;

namespace RandomData.Api.Tests.NumberGenerators
{
    public class NumberParametersValidatorTests
    {
        [Fact]
        public void NumberParametersValidator_WithDefaultParameters_IsValid()
        {
            //arrange
            var validator = new NumberParametersValidator();
            var parameters = new NumberParameters();

            //act
            var result = validator.Validate(parameters);

            //assert 
            Assert.True(result.IsValid);
        }

        [Fact]
        public void NumberParametersValidator_WithLettersInParameters_IsNotValid()
        {
            //arrange
            var validator = new NumberParametersValidator();
            var parameters = new NumberParameters()
            {
                AllowedDigits = "sd45"
            };

            //act
            var result = validator.Validate(parameters);

            //assert 
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void NumberParametersValidator_WithMinHigherThanMax_IsNotValid()
        {
            //arrange
            var validator = new NumberParametersValidator();
            var parameters = new NumberParameters()
            {
                MaxLength = 2,
                MinLength = 3
            };

            //act
            var result = validator.Validate(parameters);

            //assert 
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void NumberParametersValidator_WithMinLowerThanOne_IsNotValid()
        {
            //arrange
            var validator = new NumberParametersValidator();
            var parameters = new NumberParameters()
            {
                MinLength = 0
            };

            //act
            var result = validator.Validate(parameters);

            //assert 
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }
    }
}
