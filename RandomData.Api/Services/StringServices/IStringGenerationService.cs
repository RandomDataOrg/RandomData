using RandomData.Api.Services.StringServices.Dto;
using RandomData.Api.Services.StringServices.Enums;

namespace RandomData.Api.Services.StringServices
{
    public interface IStringGenerationService
    {
        const string DefaultAllowedCharacters =
            " !\"#$%&\'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";

        string GenerateRandomString(GetRandomStringParameters parameters);
    }
}