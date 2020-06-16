using FluentAssertions;
using RandomData.Api.GuidGenerators;
using System.Linq;
using Xunit;

namespace RandomData.Api.Tests.GuidGenerators
{
	public class GetRandomGuidParametersValidatorTests
	{
		[Fact]
		public void DefaultParameters_Valid()
		{
			//arrange
			var parameters = new GetRandomGuidParameters();
			var validator = new GetRandomGuidParametersValidator();

			//act
			var validationResult = validator.Validate(parameters);

			//assert
			validationResult.IsValid.Should().BeTrue();
		}

		[Fact]
		public void DefaultFormatCombinedWithOtherFormat_InvalidAndHasError()
		{
			//arrange
			var parameters = new GetRandomGuidParameters
			{
				Formats = new[] { RandomGuidFormats.Default, RandomGuidFormats.Uppercase }
			};
			var validator = new GetRandomGuidParametersValidator();

			//act
			var validationResult = validator.Validate(parameters);

			//assert
			validationResult.IsValid.Should().BeFalse();
			validationResult.Errors.Should().NotBeEmpty();
			validationResult.Errors.First().ErrorCode.Should().Be(GetRandomGuidParametersValidator.ErrorCode);
			validationResult.Errors.First().ErrorMessage.Should().Be($"{nameof(RandomGuidFormats.Default)} format cannot be combined with any other format.");
		}
	}
}
