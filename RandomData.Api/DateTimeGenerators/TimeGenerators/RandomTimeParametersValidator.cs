using FluentValidation;
using System;
using System.Text.RegularExpressions;

namespace RandomData.Api.DateTimeGenerators.TimeGenerators
{
    public class RandomTimeParametersValidator : AbstractValidator<RandomTimeParameters>
    {
        private static readonly Regex twentyFourFormat  =  new Regex(@"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$");
        private static readonly Regex twentyFourWithSeconds =  new Regex(@"(?:[01]\d|2[0123]):(?:[012345]\d):(?:[012345]\d$)");

        private readonly Func<string, bool> BeAValidTime = (time) => 
            (!(twentyFourFormat.IsMatch(time) || twentyFourWithSeconds.IsMatch(time))) ? false : true;

        public RandomTimeParametersValidator()
        {
            RuleFor(x => x.MinTime).Must(BeAValidTime)
            .WithMessage("The min time format is incorrect.");

            RuleFor(x => x.MaxTime).Must(BeAValidTime)
            .WithMessage("The max time format is incorrect.");
        }
    }
}
