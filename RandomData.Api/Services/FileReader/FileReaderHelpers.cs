using Microsoft.Extensions.DependencyInjection;
using RandomData.Api.Services.FileReader.ServiceImplementations;

namespace RandomData.Api.Services.FileReader
{
    public static class FileReaderHelpers
    {
        public static IServiceCollection AddFileReaderService(this IServiceCollection services)
        {
            return services
                .AddTransient<IFileReader, SystemFileReader>();
        }
    }
}