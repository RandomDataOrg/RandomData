using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace RandomData.Api.Helpers
{
    public static class SwashbuckleServiceHelper
    {
        public static IServiceCollection AddSwashbuckle(this IServiceCollection serviceCollection) =>
            serviceCollection.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "RandomData",
                    Version = "v1"
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                setup.IncludeXmlComments(xmlPath);
            });

        public static IApplicationBuilder UseSwashbuckle(this IApplicationBuilder applicationBuilder) =>
            applicationBuilder
                .UseSwagger()
                .UseSwaggerUI(setup =>
                {
                    setup.SwaggerEndpoint("/swagger/v1/swagger.json", "RandomData v1");
                });
    }
}