using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace RandomData.Api.DateTimeGenerators
{
    public class InvalidParameterException : Exception
    {
        public string Details { get; }

        public InvalidParameterException(IEnumerable<ValidationFailure> validationResultErrors) :
            base("Invalid request parameters.")
        {
            this.Details = string.Join(Environment.NewLine, validationResultErrors);
        }
    }
}
