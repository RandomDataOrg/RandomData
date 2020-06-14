using System.IO;

namespace RandomData.Api.Services.FileReaderService.ServiceImplementations
{
    public class SystemFileReaderService : IFileReaderService
    {
        public string GetFileContent(string path)
        {
            if (!File.Exists(path) || string.IsNullOrWhiteSpace(path))
            {
                throw new FileNotFoundException();
            }

            return File.ReadAllText(path);
        }
    }
}