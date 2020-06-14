namespace RandomData.Api.Services.FileReaderService.ServiceImplementations
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