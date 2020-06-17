using RandomData.Api.Services.FileReaderService;

namespace RandomData.Api.Tests.Services.FileReaderService
{
    public class FakeFileReaderService : IFileReaderService
    {
        private readonly string _content;

        public FakeFileReaderService(string content)
        {
            _content = content;
        }

        public string GetFileContent(string path)
        {
            return _content;
        }
    }
}