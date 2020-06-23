using System;

namespace RandomData.Api.DateTimeGenerators
{
    public class DateTimeGenerator
    {
        private readonly IRandomGenerator _random;
        private readonly RandomDateTimeParametersValidator _validator;

        public DateTimeGenerator(IRandomGenerator randomGenerator, RandomDateTimeParametersValidator validator)
        {
            _random = randomGenerator;
            _validator = validator;
        }

        public string Generate(RandomDateTimeParameters parameters)
        {
            var validationResult = _validator.Validate(parameters);
            if (!validationResult.IsValid)
                throw new InvalidParameterException(validationResult.Errors);

            var min = parameters.MinDateTime ?? new DateTime(1970, 1, 1);
            var max = parameters.MaxDateTime ?? DateTime.Now;
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
