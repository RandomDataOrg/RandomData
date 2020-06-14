using RandomData.Api.Services.StringServices.Enums;

namespace RandomData.Api.Services.StringServices
{
    public interface IStringGenerationService
    {
        const string DefaultAllowedCharacters =
            " !\"#$%&\'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";

        string GenerateRandomString(int minLength = 1, int maxLength = int.MaxValue,
            string allowedCharacters = DefaultAllowedCharacters, Format format = Format.Default,
            Encoding encoding = Encoding.None);

        string GenerateRandomString(int length, string allowedCharacters = DefaultAllowedCharacters,
            Format format = Format.Default,
            Encoding encoding = Encoding.None);
    }
}