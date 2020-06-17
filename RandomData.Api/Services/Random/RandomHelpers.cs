using Microsoft.Extensions.DependencyInjection;

namespace RandomData.Api.Services.Random
{
    public static class RandomHelpers
    {
        public static IServiceCollection AddRandomService(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddTransient<IRandom, Random.ServiceImplementations.Random>();
        }
    }
}