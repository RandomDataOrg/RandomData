using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RandomData.Api.StringGeneration.ServiceImplementations;
using RandomData.Api.StringGeneration.Validations;

namespace RandomData.Api.StringGeneration
{
    public static class StringGenerationServiceHelpers
    {
        public static IServiceCollection AddStringGenerationServices(this IServiceCollection services)
        {
            return services
                .AddTransient<WordsStringGenerationService>()
                .AddTransient<RandomStringGenerationService>()
                .AddTransient<StringGenerationDtoValidator>()
                .AddTransient(serviceProvider => serviceProvider.GetService<IConfiguration>()
                    .GetSection("StringGenerationOptions").Get<StringGenerationServiceOptions>());
        }

        public class StringGenerationServiceOptions
        {
            public string WordsDictionaryLocation { get; set; }
        }
    }
}