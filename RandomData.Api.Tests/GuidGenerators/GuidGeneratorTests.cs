using FluentAssertions;
using RandomData.Api.GuidGenerators;
using System;
using Xunit;

namespace RandomData.Api.Tests.GuidGenerators
{
	public class GuidGeneratorTests
	{
		[Fact]
		public void GetRandomWithDefaultParameters_NotEmptyGuid()
		{
			//arrange
			var generator = new GuidGenerator();
			var parameters = new GetRandomGuidParameters();

			//act
			var randomGuid = generator.GetRandom(parameters);

			//assert
			randomGuid.Should().NotBeEquivalentTo(Guid.Empty.ToString());
		}

		[Fact]
		public void GetRandomWithDefaultParameters_StringWithLowercaseAndHyphensLetters()
		{
			//arrange
			var generator = new GuidGenerator();
			var parameters = new GetRandomGuidParameters();

			//act
			var randomGuid = generator.GetRandom(parameters);

			//assert
			randomGuid.Should().MatchRegex(@"^[a-z0-9\-]*$");
		}

		[Fact]
		public void GetRandomStringWithWithoutHyphensFormat_StringWithoutHyphens()
		{
			//arrange
			var generator = new GuidGenerator();
			var parameters = new GetRandomGuidParameters
			{
				Formats = new[] { RandomGuidFormats.WithoutHyphens }
			};

			//act
			var randomGuid = generator.GetRandom(parameters);

			//assert
			randomGuid.Should().NotContainAny("-");
		}

		[Fact]
		public void GetRandomWithUppercaseFormat_StringWithUppercaseLetters()
		{
			//arrange
			var generator = new GuidGenerator();
			var parameters = new GetRandomGuidParameters
			{
				Formats = new[] { RandomGuidFormats.Uppercase }
			};

			//act
			var randomGuid = generator.GetRandom(parameters);

			//assert
			randomGuid.Should().MatchRegex(@"^[A-Z0-9\-]*$");
		}

		[Fact]
		public void GetRandomStringWithUppercaseAndWithoutHyphensFormats_StringWithUppercaseAndWithoutHyphensLetters()
		{
			//arrange
			var generator = new GuidGenerator();
			var parameters = new GetRandomGuidParameters
			{
				Formats = new[] { RandomGuidFormats.Uppercase, RandomGuidFormats.WithoutHyphens }
			};

			//act
			var randomGuid = generator.GetRandom(parameters);

			//assert
			randomGuid.Should().MatchRegex(@"^[A-Z0-9]*$");
		}
	}
}
