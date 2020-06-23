using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
