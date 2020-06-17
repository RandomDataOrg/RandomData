using FluentValidation;
using RandomData.Api.StringGeneration.Dto;

namespace RandomData.Api.StringGeneration.Validations
{
    public class StringGenerationDtoValidator : AbstractValidator<GetRandomStringParameters>
    {
        public StringGenerationDtoValidator()
        {
            RuleFor(x => x.MinLength).GreaterThanOrEqualTo(1);
            RuleFor(x => x.MaxLength).LessThanOrEqualTo(int.MaxValue);
            RuleFor(x => x.AllowedCharacters).NotNull().MinimumLength(1);
        }
    }
}