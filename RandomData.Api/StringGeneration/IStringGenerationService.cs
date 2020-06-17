using RandomData.Api.StringGeneration.Dto;

namespace RandomData.Api.StringGeneration
{
    public interface IStringGenerationService
    {
        string GenerateRandomString(GetRandomStringParameters parameters);
    }
}