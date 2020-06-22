using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace RandomData.Api.StringGeneration.Exceptions
{
    public class InvalidParametersException : Exception
    {
        public InvalidParametersException(IList<ValidationFailure> validationResultErrors) :
            base($"Parameters couldn't be validated. {string.Join(Environment.NewLine, validationResultErrors)}")
        {
        }
    }
}