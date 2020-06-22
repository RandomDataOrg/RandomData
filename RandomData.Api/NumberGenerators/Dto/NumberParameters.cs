using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RandomData.Api.NumberGenerators.Dto
{
    public class NumberParameters
    {
        private const string DefaultAllowedDigits = "123456789";

        [FromQuery(Name = "minLength")] public int MinLength { get; set; } = 1;

        [FromQuery(Name = "maxLength")] public int MaxLength { get; set; } = 5;

        [FromQuery(Name = "allowedDigits")] public string AllowedDigits { get; set; } = DefaultAllowedDigits;
    }
}
