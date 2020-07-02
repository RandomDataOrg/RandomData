using FluentValidation;
using RandomData.Api.WordGeneration.Dto;

namespace RandomData.Api.WordGeneration.Validators
{
    public class GetWordParametersValidator : AbstractValidator<GetWordParameters>
    {
        public GetWordParametersValidator()
        {
            RuleFor(x => x.MinLength).GreaterThanOrEqualTo(1);
            RuleFor(x => x.MaxLength).LessThanOrEqualTo(int.MaxValue);
            RuleFor(x => x.AllowedCharacters).NotNull().MinimumLength(1);
        }
    }
}