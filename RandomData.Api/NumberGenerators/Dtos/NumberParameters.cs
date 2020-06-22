using Microsoft.AspNetCore.Mvc;

namespace RandomData.Api.NumberGenerators.Dtos
{
    public class NumberParameters
    {
        private const string DefaultAllowedDigits = "1234567890";

        public int MinLength { get; set; } = 1;

        public int MaxLength { get; set; } = 5;

        public string AllowedDigits { get; set; } = DefaultAllowedDigits;
    }
}
