using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
