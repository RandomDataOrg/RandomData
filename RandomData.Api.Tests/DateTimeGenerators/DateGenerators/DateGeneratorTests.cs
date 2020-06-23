using FluentAssertions;
using RandomData.Api.DateTimeGenerators;
using RandomData.Api.DateTimeGenerators.DateGenerators;
using System;
using System.Collections.Generic;
using System.Globalization;
using Xunit;

namespace RandomData.Api.Tests.DateTimeGenerators.DateGenerators
{
    public class DateGeneratorTests
    {
        public DateGeneratorTests()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");
        }

        public static List<object[]> TestDates()
        {
            return new List<object[]>
            {
                new object[] {new DateTime(2000,1,1), new DateTime(2005,1,1),  RandomDateFormats.Short,"1/1/2005"},
                new object[] {new DateTime(1997,6,6), new DateTime(2003,12,12),RandomDateFormats.MonthDay,"December 12"},
                new object[] {new DateTime(1900,1,1), new DateTime(2020,6,17), RandomDateFormats.MonthDay, "June 17"},
                new object[] {new DateTime(1000,5,15),new DateTime(2256,10,25),RandomDateFormats.YearMonth,"October 2256"},
                new object[] {new DateTime(2154,4,26),new DateTime(3256,11,14),RandomDateFormats.YearMonth,"November 3256"}
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

        [Theory]
        [MemberData(nameof(TestDates))]
        public void GenerateWithCorrectParameters_StringWithDate(DateTime minDate, DateTime maxDate, RandomDateFormats format, string expected)
        {
            //Arrange
            var dateTimeGenerator = new DateTimeGenerator(new FakeRandomGenerator(), new RandomDateTimeParametersValidator());
            var dateGenerator = new DateGenerator(dateTimeGenerator);
            var parameters = new RandomDateParameters
            {
                MinDate = minDate,
                MaxDate = maxDate,
                Format = format
            };

            //Act
            var output = dateGenerator.Generate(parameters);

            //Assert
            output.Should().Be(expected);
        }


        [Theory]
        [MemberData(nameof(MinTestDatesGreaterThanMax))]
        public void GenerateWithMinDateGreaterThanMaxDate_ThrowsException(DateTime minDate, DateTime maxDate)
        {
            //Arrange
            var generator = new DateTimeGenerator(new FakeRandomGenerator(), new RandomDateTimeParametersValidator());
            var parameters = new RandomDateTimeParameters
            {
                MinDateTime = minDate,
                MaxDateTime = maxDate,
            };

            //Act
            Func<string> act = () => generator.Generate(parameters);

            //Assert
            act.Should().ThrowExactly<InvalidParameterException>().WithMessage("Invalid request parameters.");
        }
    }
}
