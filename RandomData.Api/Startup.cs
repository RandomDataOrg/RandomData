using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RandomData.Api.DateTimeGenerators;
using RandomData.Api.DateTimeGenerators.TimeGenerators;
using RandomData.Api.Extensions;
using RandomData.Api.GuidGenerators;

namespace RandomData.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddGuidGenerator();
			services.AddDateTimeGenerator();
			services.AddTimeGenerator();
			services.RegisterProblemDetails();
			services.AddSwaggerWithConfig(Configuration);
			services.AddControllers();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			
			if(env.IsProduction())
			{
				app.UseProblemDetails();
			}

			app.UseHttpsRedirection();
      
			app.UseSwaggerWithConfig(Configuration);
      
			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
