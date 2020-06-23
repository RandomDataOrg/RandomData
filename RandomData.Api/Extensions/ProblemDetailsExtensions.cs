using Hellang.Middleware.ProblemDetails;
using Microsoft.Extensions.DependencyInjection;
using RandomData.Api.DateTimeGenerators;
using RandomData.Api.ProblemDetails;

namespace RandomData.Api.Extensions
{
    public static class ProblemDetailsExtensions
    {
        public static IServiceCollection RegisterProblemDetails(this IServiceCollection services)
        {
            return services.AddProblemDetails(x =>
            {
                x.Map<InvalidParameterException>(ex => new InvalidParameterProblemDetails(ex));
            });
        }
    }
}