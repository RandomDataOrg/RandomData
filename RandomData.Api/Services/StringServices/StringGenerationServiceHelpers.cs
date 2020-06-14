using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RandomData.Api.Services.StringServices.Enums;
using RandomData.Api.Services.StringServices.ServiceImplementations;

namespace RandomData.Api.Services.StringServices
{
    public static class StringGenerationServiceHelpers
    {
        public delegate IStringGenerationService StringGenerationServiceResolver(GenerationTypes key);
        
        public static IServiceCollection AddStringGenerationServices(this IServiceCollection services) => services
            .AddTransient<WordsStringGenerationService>()
            .AddTransient<RandomStringGenerationService>()
            .AddTransient(serviceProvider => serviceProvider.GetService<IConfiguration>().GetSection("StringGenerationOptions").Get<StringGenerationServiceOptions>())
            .AddTransient<StringGenerationServiceResolver>(serviceProvider => key =>
            {
                return key switch
                {
                    GenerationTypes.Words => serviceProvider.GetService<WordsStringGenerationService>(),
                    GenerationTypes.Random => serviceProvider.GetService<RandomStringGenerationService>(),
                    _ => throw new KeyNotFoundException()
                };
            });

        public class StringGenerationServiceOptions
        {
            public string WordsDictionaryLocation { get; set; }
        }
    }
}