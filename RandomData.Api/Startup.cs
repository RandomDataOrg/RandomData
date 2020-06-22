using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RandomData.Api.Extensions;
using RandomData.Api.Services.FileReader;
using RandomData.Api.Services.Random;
using RandomData.Api.StringGeneration;

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
			services.AddMemoryCache();
			services.AddSwaggerWithConfig(Configuration.GetSection("SwaggerConfig").Get<OpenApiInfo>());
			services.AddFileReaderService();
			services.AddRandomService();
			services.AddStringGenerationServices();
			services.AddControllers();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();
      
			app.UseSwaggerWithConfig();
      
			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}