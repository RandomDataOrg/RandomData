using Microsoft.Extensions.DependencyInjection;

namespace RandomData.Api.DateTimeGenerators.DateGenerators
{
    public static class DateGeneratorExtensions
    {
		public static IServiceCollection AddDateGenerator(this IServiceCollection services)
		{
			services.AddTransient<DateGenerator>();
			return services;
		}
	}
}
