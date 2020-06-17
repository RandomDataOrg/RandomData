using Microsoft.Extensions.DependencyInjection;
using RandomData.Api.Services.FileReaderService.ServiceImplementations;

namespace RandomData.Api.Services.FileReaderService
{
    public static class FileReaderServiceHelpers
    {
        public static IServiceCollection AddFileReaderService(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddTransient<IFileReaderService, SystemFileReaderService>();
        }
    }
}