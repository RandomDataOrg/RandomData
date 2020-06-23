using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RandomData.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerWithConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var apiInfo = GetApiInfo(configuration);
            return services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(apiInfo.Version, apiInfo);
                options.AddXmlComments();
            });
        }

        public static IApplicationBuilder UseSwaggerWithConfig(this IApplicationBuilder app, IConfiguration configuration)
        {
            var apiInfo = GetApiInfo(configuration);
            return app
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint($"/swagger/{apiInfo.Version}/swagger.json", $"{apiInfo.Title} {apiInfo.Version}");
                    options.RoutePrefix = string.Empty;
                });
        }

        private static void AddXmlComments(this SwaggerGenOptions options)
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        }

        private static OpenApiInfo GetApiInfo(IConfiguration configuration)
            => configuration.GetSection("SwaggerConfig").Get<OpenApiInfo>();
    }
}