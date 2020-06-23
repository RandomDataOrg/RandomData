﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomData.Api.DateTimeGenerators.DateGenerators
{
    public class RandomDateParameters
    {
        public DateTime? MinDate { get; set; } = null;
        public DateTime? MaxDate { get; set; } = null;
        public RandomDateFormats Format { get; set; } = RandomDateFormats.Short;
    }
}
