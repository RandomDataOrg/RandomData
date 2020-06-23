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
            RuleFor(m => m.MaxDate)
                .GreaterThan(m => m.MinDate.Value)
                                .WithMessage("Max date must be greater than min date.")
                .When(m => m.MinDate.HasValue);

            RuleFor(m => m.Format).Must(m => _correctFormats.Contains(m))
                .WithMessage(m => $"Format '{m.Format}' is not supported.");
        }
    }
}
