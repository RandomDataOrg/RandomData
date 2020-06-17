using FluentValidation;
using RandomData.Api.StringGeneration.Dto;

namespace RandomData.Api.StringGeneration.Validators
{
    public class GetStringParametersValidator : AbstractValidator<GetStringParameters>
    {
        public GetStringParametersValidator()
        {
            RuleFor(x => x.MinLength).GreaterThanOrEqualTo(1);
            RuleFor(x => x.MaxLength).LessThanOrEqualTo(int.MaxValue);
            RuleFor(x => x.AllowedCharacters).NotNull().MinimumLength(1);
        }
    }
}