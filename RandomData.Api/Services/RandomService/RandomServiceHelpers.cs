using Microsoft.Extensions.DependencyInjection;

namespace RandomData.Api.Services.RandomService
{
    public static class RandomServiceHelpers
    {
        public static IServiceCollection AddRandomService(this IServiceCollection serviceCollection) =>
            serviceCollection.AddTransient<IRandom, ServiceImplementations.Random>();
    }
}