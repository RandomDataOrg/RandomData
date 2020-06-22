using FluentValidation;
using RandomData.Api.NumberGenerators.Dtos;

namespace RandomData.Api.NumberGenerators.Validators
{
    public interface INumberParametersValidator : IValidator<NumberParameters>
    {
    }
}
