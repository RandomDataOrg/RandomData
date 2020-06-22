﻿using System;
using System.Linq;
using FluentValidation;
using RandomData.Api.NumberGenerators.Dto;
using RandomData.Api.NumberGenerators.Validators;

namespace RandomData.Api.NumberGenerators.Validator
{
    public class NumberParametersValidator : AbstractValidator<NumberParameters>, INumberParametersValidator
    {
        public NumberParametersValidator()
        {
            RuleFor(x => x.MinLength).GreaterThanOrEqualTo(1);
            RuleFor(x => x.MaxLength).LessThanOrEqualTo(int.MaxValue);
            RuleFor(x => x.MaxLength).GreaterThanOrEqualTo(x => x.MinLength);
            RuleFor(x => x.AllowedDigits).MinimumLength(1);
            RuleFor(x => x.AllowedDigits).Must(value => value.All(char.IsDigit));
        }
    }
}
