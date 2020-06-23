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
        public void GenerateWithCorrectParameters_StringWithDateTime(DateTime minDate, DateTime maxDate, string format, string expected)
        {
            //Arrange
            var generator = new DateTimeGenerator(new FakeRandomGenerator(),new RandomDateTimeParametersValidator());
            var parameters = new RandomDateTimeParameters
            {
                MinDateTime = minDate,
                MaxDateTime = maxDate,
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
        public void GenerateWithMinDateTimeGreaterThanMaxDateTime_ThrowsException(DateTime minDateTime, DateTime maxDateTime)
        {
            //Arrange
            var generator = new DateTimeGenerator(new FakeRandomGenerator(), new RandomDateTimeParametersValidator());
            var parameters = new RandomDateTimeParameters
            {
                MinDateTime = minDateTime,
                MaxDateTime = maxDateTime,
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
                new object[] {new DateTime(2000,1,1,10,20,30), new DateTime(2005,3,1,9,20,0), "g","3/1/2005 9:20 AM"},
                new object[] {new DateTime(1997,6,6,20,30,50), new DateTime(2003,12,12,2,3,5),"g","12/12/2003 2:03 AM"},
                new object[] {new DateTime(1900,1,1), new DateTime(2020,6,17), "g","6/17/2020 12:00 AM"},
                new object[] {new DateTime(1000,5,15,17,45,23),new DateTime(2256,10,25,13,30,30),"G","10/25/2256 1:30:30 PM"},
                new object[] {new DateTime(2154,4,26,23,59,57),new DateTime(3256,12,14,23,59,59),"G","12/14/3256 11:59:59 PM"}
            };
        }

        public static List<object[]> MinTestDateTimeGreaterThanMax()
        {
            return new List<object[]>
            {
                new object[] { new DateTime(2005,1,1,10,20,30),  new DateTime(2000,1,1,17,45,23) },
                new object[] { new DateTime(2003,12,12),new DateTime(1997,6,6,17,45,23) },
                new object[] { new DateTime(2020,6,17), new DateTime(1900,1,1)},
                new object[] { new DateTime(2256,10,25,23,59,57),new DateTime(1000,5,15)},
                new object[] { new DateTime(3256,12,14),new DateTime(2154,4,26)}
            };
        }
    }
}
