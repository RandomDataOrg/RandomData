using Microsoft.Extensions.DependencyInjection;
using RandomData.Api.NumberGenerators.Generators;
using RandomData.Api.NumberGenerators.Validator;
using RandomData.Api.NumberGenerators.Validators;

namespace RandomData.Api.NumberGenerators.ServicesExtensions
{
    public static class NumberGeneratorExtensions
    {
        public static IServiceCollection AddNumberGenerator(this IServiceCollection services)
        {
            services.AddSingleton<INumberParametersValidator, NumberParametersValidator>();
            services.AddSingleton<INumberGenerator, NumberGenerator>();

            return services;
        }
    }
}
