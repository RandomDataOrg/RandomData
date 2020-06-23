using FluentValidation;
using System;
using System.Linq;

namespace RandomData.Api.DateTimeGenerators
{
    public class RandomDateTimeParametersValidator : AbstractValidator<RandomDateTimeParameters>
    {
        private readonly string[] _correctFormats = new[] {"d", "D", "f", "F", "g", "G",
                                      "m", "M", "o", "O", "r", "R", "s", "t", "T",
                                      "u", "U", "y", "Y"};

        public RandomDateTimeParametersValidator()
        {
            RuleFor(m => m.MaxDateTime)
                .GreaterThan(m => m.MinDateTime.Value)
                    .WithMessage("Max datetime must be greater than min datetime.")
                .When(m => m.MinDateTime.HasValue);

            RuleFor(m => m.Format).Must(m => _correctFormats.Contains(m))
                .WithMessage(m => $"Format '{m.Format}' is not supported.");
        }
    }
}