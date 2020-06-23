using System;

namespace RandomData.Api.DateTimeGenerators
{
    public class RandomDateTimeParameters
    {
        public DateTime? MinDate { get; set; } = null;
        public DateTime? MaxDate { get; set; } = null;
        public string Format { get; set; } = "G";
    }
}
