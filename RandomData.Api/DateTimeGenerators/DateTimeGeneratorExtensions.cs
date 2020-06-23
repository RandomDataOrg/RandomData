using Microsoft.Extensions.DependencyInjection;

namespace RandomData.Api.DateTimeGenerators
{
    public static class DateTimeGeneratorExtensions
    {
		public static IServiceCollection AddDateTimeGenerator(this IServiceCollection services)
		{
			services.AddTransient<DateTimeGenerator>();
			services.AddTransient<IRandomGenerator, RandomGenerator>();
			services.AddTransient<RandomDateTimeParametersValidator>();

			return services;
		}
	}
}
