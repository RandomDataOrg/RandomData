using Microsoft.Extensions.DependencyInjection;
using RandomData.Api.StringGeneration.Validators;

namespace RandomData.Api.StringGeneration
{
    public static class StringGenerationExtensions
    {
        public static IServiceCollection AddStringGenerationService(this IServiceCollection services)
        {
            return services
                .AddTransient<StringGeneration>()
                .AddTransient<GetStringParametersValidator>();
        }
    }
}