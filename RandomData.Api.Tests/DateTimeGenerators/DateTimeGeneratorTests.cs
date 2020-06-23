using FluentAssertions;
using RandomData.Api.DateTimeGenerators;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Xunit;

namespace RandomData.Api.Tests.DateTimeGenerators
{
    public class DateTimeGeneratorTests
    {
        public DateTimeGeneratorTests()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");
        }

        [Theory]
        [MemberData(nameof(TestDates))]
        public void GenerateWithCorrectParameters_StringWithDate(DateTime minDate, DateTime maxDate, string format, string expected)
        {
            //Arrange
            var generator = new DateTimeGenerator(new FakeRandomGenerator(),new RandomDateTimeParametersValidator());
            var parameters = new RandomDateTimeParameters
            {
                MinDate = minDate,
                MaxDate = maxDate,
                Format = format
            };

            //Act
            var output = generator.Generate(parameters);

            //Assert
            output.Should().Be(expected);
        }

        [Theory]
        [InlineData("xd")]
        [InlineData("xD")]
        [InlineData("j")]
        [InlineData("h")]
        [InlineData("dupa")]
        [InlineData("P")]
        [InlineData("Q")]
        public void GenerateWithIncorectFormat_ThrowsException(string format)
        {
            //Arrange
            var generator = new DateTimeGenerator(new FakeRandomGenerator(), new RandomDateTimeParametersValidator());
            var parameters = new RandomDateTimeParameters
            {
                Format = format
            };

            //Act
            Func<string> act = () => generator.Generate(parameters);

            //Assert
            act.Should().ThrowExactly<InvalidParameterException>().WithMessage("Invalid request parameters.");
        }

        [Theory]
        [MemberData(nameof(MinTestDatesGreaterThanMax))]
        public void GenerateWithMinDateGreaterThanMaxDate_ThrowsException(DateTime minDate, DateTime maxDate)
        {
            //Arrange
            var generator = new DateTimeGenerator(new FakeRandomGenerator(), new RandomDateTimeParametersValidator());
            var parameters = new RandomDateTimeParameters
            {
                MinDate = minDate,
                MaxDate = maxDate,
            };

            //Act
            Func<string> act = () => generator.Generate(parameters);

            //Assert
            act.Should().ThrowExactly<InvalidParameterException>().WithMessage("Invalid request parameters.");
        }

        public static List<object[]> TestDates()
        {
            return new List<object[]>
            {
                new object[] {new DateTime(2000,1,1), new DateTime(2005,1,1),  "d","1/1/2005"},
                new object[] {new DateTime(1997,6,6), new DateTime(2003,12,12),"d","12/12/2003"},
                new object[] {new DateTime(1900,1,1), new DateTime(2020,6,17), "d","6/17/2020"},
                new object[] {new DateTime(1000,5,15),new DateTime(2256,10,25),"d","10/25/2256"},
                new object[] {new DateTime(2154,4,26),new DateTime(3256,12,14),"d","12/14/3256"}
            };
        }

        public static List<object[]> MinTestDatesGreaterThanMax()
        {
            return new List<object[]>
            {
                new object[] { new DateTime(2005,1,1),  new DateTime(2000,1,1)},
                new object[] { new DateTime(2003,12,12),new DateTime(1997,6,6)},
                new object[] { new DateTime(2020,6,17), new DateTime(1900,1,1)},
                new object[] { new DateTime(2256,10,25),new DateTime(1000,5,15)},
                new object[] { new DateTime(3256,12,14),new DateTime(2154,4,26)}
            };
        }
    }
}
