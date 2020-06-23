using System;

namespace RandomData.Api.DateTimeGenerators
{
    public class RandomDateTimeParameters
    {
        public DateTime? MinDateTime { get; set; } = null;
        public DateTime? MaxDateTime { get; set; } = null;
        public string Format { get; set; } = "G";
    }
}
