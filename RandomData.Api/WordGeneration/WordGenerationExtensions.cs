using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RandomData.Api.WordGeneration.Configuration;
using RandomData.Api.WordGeneration.Validators;

namespace RandomData.Api.WordGeneration
{
    public static class WordGenerationExtensions
    {
        public static IServiceCollection AddWordGenerationService(this IServiceCollection services)
        {
            return services
                .AddTransient<WordGeneration>()
                .AddTransient<GetWordParametersValidator>()
                .AddTransient(serviceProvider => serviceProvider.GetService<IConfiguration>()
                    .GetSection("StringGenerationOptions").Get<WordsGenerationOptions>());
        }
    }
}