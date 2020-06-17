using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace RandomData.Api.StringGeneration.Exceptions
{
    public class InvalidParametersException : Exception
    {
        public InvalidParametersException(IList<ValidationFailure> validationResultErrors) :
            base("Parameters couldn't be validated. " +
                 $"{validationResultErrors.Select(x => x.ToString()).Aggregate((s, s1) => s + "\n" + s1)}")
        {
        }
    }
}