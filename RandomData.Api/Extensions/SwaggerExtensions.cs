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
        public static IServiceCollection AddSwaggerWithConfig(this IServiceCollection services, OpenApiInfo apiInfo) =>
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", apiInfo);
                options.AddXmlComments();
            });

        private static void AddXmlComments(this SwaggerGenOptions options)
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        }

        public static IApplicationBuilder UseSwaggerWithConfig(this IApplicationBuilder app) =>
            app
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "RandomData v1");
                    options.RoutePrefix = string.Empty;
                });
    }
}