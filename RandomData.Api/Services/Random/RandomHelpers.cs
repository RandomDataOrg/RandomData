using Microsoft.Extensions.DependencyInjection;
using RandomData.Api.Services.Random.ServiceImplementations;

namespace RandomData.Api.Services.Random
{
    public static class RandomHelpers
    {
        public static IServiceCollection AddRandomService(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddTransient<IRandomGenerator, RandomGenerator>();
        }
    }
}