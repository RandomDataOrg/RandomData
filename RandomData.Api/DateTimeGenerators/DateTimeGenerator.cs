using System;

namespace RandomData.Api.DateTimeGenerators
{
    public class DateTimeGenerator
    {
        private readonly IRandomGenerator _random;

        public DateTimeGenerator(IRandomGenerator randomGenerator)
        {
            _random = randomGenerator;
        }

        public string Generate(RandomDateTimeParameters parameters)
        {
            var min = parameters.MinDate ?? new DateTime(1970, 1, 1);
            var max = parameters.MaxDate ?? DateTime.Now;

            var totalTicks = max.Ticks - min.Ticks;

            var randomTimeSpan = GetRandomTimeSpan(totalTicks);

            return (new DateTime(min.Ticks) + randomTimeSpan).ToString(parameters.Format);
        }


        private TimeSpan GetRandomTimeSpan(long totalTimeSpanTicks)
        {
            var partTimeSpanTicks = _random.NextDouble() * totalTimeSpanTicks;
            return TimeSpan.FromTicks(Convert.ToInt64(partTimeSpanTicks));
        }
    }
}
