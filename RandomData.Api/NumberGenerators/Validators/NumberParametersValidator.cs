using System.Linq;
using FluentValidation;
using RandomData.Api.NumberGenerators.Dtos;

namespace RandomData.Api.NumberGenerators.Validators
{
    public class NumberParametersValidator : AbstractValidator<NumberParameters>, INumberParametersValidator
    {
        public NumberParametersValidator()
        {
            RuleFor(x => x.MinLength).GreaterThanOrEqualTo(1);
            RuleFor(x => x.MaxLength).LessThanOrEqualTo(int.MaxValue)
                .GreaterThanOrEqualTo(x => x.MinLength);
            RuleFor(x => x.AllowedDigits).MinimumLength(1)
                .Must(value => value.All(char.IsDigit));
        }
    }
}
