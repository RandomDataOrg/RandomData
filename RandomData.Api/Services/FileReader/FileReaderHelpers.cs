using Microsoft.Extensions.DependencyInjection;
using RandomData.Api.Services.FileReader.ServiceImplementations;

namespace RandomData.Api.Services.FileReader
{
    public static class FileReaderHelpers
    {
        public static IServiceCollection AddFileReaderService(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddTransient<IFileReader, SystemFileReader>();
        }
    }
}