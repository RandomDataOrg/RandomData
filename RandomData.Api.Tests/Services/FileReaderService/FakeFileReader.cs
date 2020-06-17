using RandomData.Api.Services.FileReader;

namespace RandomData.Api.Tests.Services.FileReaderService
{
    public class FakeFileReader : IFileReader
    {
        private readonly string _content;

        public FakeFileReader(string content)
        {
            _content = content;
        }

        public string GetFileContent(string path)
        {
            return _content;
        }
    }
}