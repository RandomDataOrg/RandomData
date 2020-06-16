using FluentValidation;
using RandomData.Api.Services.StringServices.Enums;

namespace RandomData.Api.Services.StringServices.Dto
{
    public class StringGenerationServiceDto
    {
        private const string DefaultAllowedCharacters =
            " !\"#$%&\'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
        
        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        public string AllowedCharacters { get; set; }
        public Format Format { get; set; }
        public Encoding Encoding { get; set; }

        public StringGenerationServiceDto(int minLength = 1, int maxLength = int.MaxValue,
            string allowedCharacters = DefaultAllowedCharacters, Format format = Format.Default,
            Encoding encoding = Encoding.None)
        {
            MinLength = minLength;
            MaxLength = maxLength;
            AllowedCharacters = allowedCharacters;
            Format = format;
            Encoding = encoding;
        }

        public StringGenerationServiceDto(int length, string allowedCharacters = DefaultAllowedCharacters,
            Format format = Format.Default,
            Encoding encoding = Encoding.None)
        {
            MinLength = length;
            MaxLength = length;
            AllowedCharacters = allowedCharacters;
            Format = format;
            Encoding = encoding;
        }
    }

    public class StringGenerationServiceDtoValidator : AbstractValidator<StringGenerationServiceDto>
    {
        public StringGenerationServiceDtoValidator()
        {
            RuleFor(x => x.MinLength).GreaterThanOrEqualTo(1);
            RuleFor(x => x.MaxLength).LessThanOrEqualTo(int.MaxValue);
            RuleFor(x => x.AllowedCharacters).NotNull();
        }
    }
}