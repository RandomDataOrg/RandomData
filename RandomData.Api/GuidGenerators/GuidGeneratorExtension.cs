using Microsoft.Extensions.DependencyInjection;

namespace RandomData.Api.GuidGenerators
{
	public static class GuidGeneratorExtension
	{
		public static IServiceCollection AddGuidGenerator(this IServiceCollection services)
		{
			services.AddTransient<IGuidGenerator, GuidGenerator>();
			services.AddTransient<IGetRandomGuidParametersValidator, GetRandomGuidParametersValidator>();
			return services;
		}
	}
}
