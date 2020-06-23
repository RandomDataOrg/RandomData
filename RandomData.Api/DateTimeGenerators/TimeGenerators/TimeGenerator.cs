using System;

namespace RandomData.Api.DateTimeGenerators.TimeGenerators
{
    public class TimeGenerator
    {
        private readonly DateTimeGenerator _dateTimeGenerator;

        public TimeGenerator(DateTimeGenerator dateTimeGenerator)
        {
            _dateTimeGenerator = dateTimeGenerator;
        }

        public string Generate(RandomTimeParameters parameters)
        {
            var min = Convert.ToDateTime(parameters.MinTime);
            var max = Convert.ToDateTime(parameters.MaxTime);
            var format = GetFormatedTime(parameters.Format);

            var dateTimeParameters = new RandomDateTimeParameters
            {
                MinDate = min,
                MaxDate = max,
                Format = format
            };

            return _dateTimeGenerator.Generate(dateTimeParameters);
        }

        private string GetFormatedTime(RandomTimeFormats format) =>
            format switch
            {
                RandomTimeFormats.Short => "t",
                RandomTimeFormats.Long => "T",
                _ => throw new ArgumentOutOfRangeException(nameof(format), format, null)
            };
    }
}
