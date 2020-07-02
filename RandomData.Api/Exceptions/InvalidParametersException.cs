using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace RandomData.Api.Exceptions
{
    public class InvalidParametersException : Exception
    {
        public InvalidParametersException(IEnumerable<ValidationFailure> validationResultErrors) :
            base($"Parameters couldn't be validated. {string.Join(Environment.NewLine, validationResultErrors)}")
        {
        }
    }
}