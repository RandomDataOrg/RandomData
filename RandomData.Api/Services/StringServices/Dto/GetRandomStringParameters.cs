using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RandomData.Api.Services.StringServices.Enums;

namespace RandomData.Api.Services.StringServices.Dto
{
    public class GetRandomStringParameters
    {
        private const string DefaultAllowedCharacters =
            " !\"#$%&\'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
        
        /// <summary>
        /// Minimum length of created string (ignored if length is set); Default value is 1
        /// </summary>
        [FromQuery(Name = "minLength")] 
        public int MinLength { get; set; } = 1;
        
        /// <summary>
        /// Maximum length of created string (ignored if length is set); Default value is 100
        /// </summary>
        [FromQuery(Name = "maxLength")]
        public int MaxLength { get; set; } = 100;
        
        /// <summary>
        /// Characters from which string will be built; By default all available ASCII chars
        /// </summary>
        [FromQuery(Name = "allowedCharacters")]
        public string AllowedCharacters { get; set; } = DefaultAllowedCharacters;
        
        /// <summary>
        /// Format of returned string
        /// </summary>
        [FromQuery(Name = "format")] 
        public Format Format { get; set; } = Format.Default;
        
        /// <summary>
        /// Encoding format of returned string
        /// </summary>
        [FromQuery(Name = "encoding")] 
        public Encoding Encoding { get; set; } = Encoding.None;
    }

    public class StringGenerationServiceDtoValidator : AbstractValidator<GetRandomStringParameters>
    {
        public StringGenerationServiceDtoValidator()
        {
            RuleFor(x => x.MinLength).GreaterThanOrEqualTo(1);
            RuleFor(x => x.MaxLength).LessThanOrEqualTo(int.MaxValue);
            RuleFor(x => x.AllowedCharacters).NotNull();
        }
    }
}