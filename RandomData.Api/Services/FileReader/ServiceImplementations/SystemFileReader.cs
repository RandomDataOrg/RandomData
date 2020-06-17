using System.IO;

namespace RandomData.Api.Services.FileReader.ServiceImplementations
{
    public class SystemFileReader : IFileReader
    {
        public string GetFileContent(string path)
        {
            if (!File.Exists(path) || string.IsNullOrWhiteSpace(path)) throw new FileNotFoundException();

            return File.ReadAllText(path);
        }
    }
}