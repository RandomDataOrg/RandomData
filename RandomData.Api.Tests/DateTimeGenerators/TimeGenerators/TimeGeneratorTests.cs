using FluentAssertions;
using RandomData.Api.DateTimeGenerators;
using RandomData.Api.DateTimeGenerators.TimeGenerators;
using System;
using System.Collections.Generic;
using System.Globalization;
using Xunit;

namespace RandomData.Api.Tests.DateTimeGenerators.TimeGenerators
{
    public class TimeGeneratorTests
    {
        public TimeGeneratorTests()
        {
           CultureInfo.CurrentCulture = new CultureInfo("en-US");
        }

        [Theory]
        [InlineData("06:00:00", "07:00:00", RandomTimeFormats.Short, "7:00 AM")]
        [InlineData("14:00:00", "17:00:00", RandomTimeFormats.Long, "5:00:00 PM")]
        [InlineData("23:59:58", "23:59:59", RandomTimeFormats.Long, "11:59:59 PM")]
        [InlineData("18:00:00", "18:00:30", RandomTimeFormats.Short, "6:00 PM")]
        [InlineData("14:32:52", "15:38:41", RandomTimeFormats.Long, "3:38:41 PM")]
        public void GenerateWithCorrectParameters_StringWithTime(string minDate, string maxDate, RandomTimeFormats format, string expected)
        {
            //Arrange
            var dateTimeGenerator = new DateTimeGenerator(new FakeRandomGenerator(), new RandomDateTimeParametersValidator());
            var timeGenerator = new TimeGenerator(dateTimeGenerator, new RandomTimeParametersValidator());
            var parameters = new RandomTimeParameters
            {
                MinTime = minDate,
                MaxTime = maxDate,
                Format = format
            };

            //Act
            var output = timeGenerator.Generate(parameters);

            //Assert
            output.Should().Be(expected);
        }

        [Theory]
        [InlineData("06:00:60", "07:00")]
        [InlineData("24:00:00", "17:30")]
        [InlineData("23:59:58", "12:595:59")]
        [InlineData("18:00:000", "18:00:30")]
        [InlineData("14:32:52", "14:422")]
        public void GenerateWithMinDateGreaterThanMaxDate_ThrowsException(string minTime, string maxTime)
        {
            //Arrange
            var dateTimeGenerator = new DateTimeGenerator(new FakeRandomGenerator(), new RandomDateTimeParametersValidator());
            var timeGenerator = new TimeGenerator(dateTimeGenerator, new RandomTimeParametersValidator());
            var parameters = new RandomTimeParameters
            {
                MinTime = minTime,
                MaxTime = maxTime
            };


            //Act
            Func<string> act = () => timeGenerator.Generate(parameters);

            //Assert
            act.Should().ThrowExactly<InvalidParameterException>().WithMessage("Invalid request parameters.");
        }
    }
}

