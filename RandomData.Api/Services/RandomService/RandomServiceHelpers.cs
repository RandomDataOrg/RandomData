using Microsoft.Extensions.DependencyInjection;
using RandomData.Api.Services.RandomService.ServiceImplementations;

namespace RandomData.Api.Services.RandomService
{
    public static class RandomServiceHelpers
    {
        public static IServiceCollection AddRandomService(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddTransient<IRandom, Random>();
        }
    }
}