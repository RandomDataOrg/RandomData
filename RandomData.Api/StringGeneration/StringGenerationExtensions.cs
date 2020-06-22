using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RandomData.Api.StringGeneration.Configuration;
using RandomData.Api.StringGeneration.ServiceImplementations;
using RandomData.Api.StringGeneration.Validators;

namespace RandomData.Api.StringGeneration
{
    public static class StringGenerationExtensions
    {
        public static IServiceCollection AddStringGenerationServices(this IServiceCollection services)
        {
            return services
                .AddTransient<WordsStringGenerationService>()
                .AddTransient<RandomStringGenerationService>()
                .AddTransient<GetStringParametersValidator>()
                .AddTransient(serviceProvider => serviceProvider.GetService<IConfiguration>()
                    .GetSection("StringGenerationOptions").Get<StringGenerationServiceOptions>());
        }
    }
}