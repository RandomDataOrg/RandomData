using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using RandomData.Api.NumberGenerators.Dto;

namespace RandomData.Api.NumberGenerators.Validators
{
    public interface INumberParametersValidator : IValidator<NumberParameters>
    {
    }
}
