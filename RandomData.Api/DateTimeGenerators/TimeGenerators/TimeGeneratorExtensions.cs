using Microsoft.Extensions.DependencyInjection;

namespace RandomData.Api.DateTimeGenerators.TimeGenerators
{
    public static class TimeGeneratorExtensions
    {
		public static IServiceCollection AddTimeGenerator(this IServiceCollection services)
		{
			services.AddTransient<TimeGenerator>();
			services.AddTransient<RandomTimeParametersValidator>();
			return services;
		}
	}
}
