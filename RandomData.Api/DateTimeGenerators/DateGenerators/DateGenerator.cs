﻿using System;

namespace RandomData.Api.DateTimeGenerators.DateGenerators
{
    public class DateGenerator
    {
        private readonly DateTimeGenerator _dateTimeGenerator;

        public DateGenerator(DateTimeGenerator dateTimeGenerator)
        {
            _dateTimeGenerator = dateTimeGenerator;
        }

        public string Generate(RandomDateParameters parameters)
        {
            var format = GetDateFormat(parameters.Format);
            var dateTimeParameters = new RandomDateTimeParameters
            {
                MinDateTime = parameters.MinDate,
                MaxDateTime = parameters.MaxDate,
                Format = format
            };
            return _dateTimeGenerator.Generate(dateTimeParameters);
        }

        private string GetDateFormat(RandomDateFormats format) =>
            format switch
            {
                RandomDateFormats.Short => "d",
                RandomDateFormats.Long => "D",
                RandomDateFormats.MonthDay => "m",
                RandomDateFormats.YearMonth => "y",
                _ => throw new ArgumentOutOfRangeException(nameof(format), format, null)
            };
    }
}
