using Microsoft.AspNetCore.Http;
using RandomData.Api.DateTimeGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomData.Api.ProblemDetails
{
    public class InvalidParameterProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public InvalidParameterProblemDetails(InvalidParameterException exception)
        {
            this.Type = "https://example.net/validation-error";
            this.Title = exception.Message;
            this.Detail = exception.Details;
            this.Status = StatusCodes.Status400BadRequest;
        }
    }
}
