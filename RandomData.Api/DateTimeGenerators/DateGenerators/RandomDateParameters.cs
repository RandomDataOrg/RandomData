using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomData.Api.DateTimeGenerators.DateGenerators
{
    public class RandomDateParameters
    {
        public DateTime? MinDateTime { get; set; } = null;
        public DateTime? MaxDateTime { get; set; } = null;
        public RandomDateFormats Format { get; set; } = RandomDateFormats.Short;
    }
}
