using System.IO;

namespace RandomData.Api.Services.FileReader.ServiceImplementations
{
    public class SystemFileReader : IFileReader
    {
        public string GetFileContent(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            return File.ReadAllText(path);
        }
    }
}