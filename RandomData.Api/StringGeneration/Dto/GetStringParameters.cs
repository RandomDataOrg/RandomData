using Microsoft.AspNetCore.Mvc;
using RandomData.Api.StringGeneration.Enums;

namespace RandomData.Api.StringGeneration.Dto
{
    public class GetStringParameters
    {
        private const string DefaultAllowedCharacters =
            " !\"#$%&\'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";

        /// <summary>
        ///     Minimum length of created string (ignored if length is set); Default value is 1
        /// </summary>
        [FromQuery(Name = "minLength")]
        public int MinLength { get; set; } = 1;

        /// <summary>
        ///     Maximum length of created string (ignored if length is set); Default value is 100
        /// </summary>
        [FromQuery(Name = "maxLength")]
        public int MaxLength { get; set; } = 100;

        /// <summary>
        ///     Characters from which string will be built; By default all available ASCII chars
        /// </summary>
        [FromQuery(Name = "allowedCharacters")]
        public string AllowedCharacters { get; set; } = DefaultAllowedCharacters;

        /// <summary>
        ///     Format of returned string
        /// </summary>
        [FromQuery(Name = "format")]
        public Format Format { get; set; } = Format.Default;

        /// <summary>
        ///     Encoding format of returned string
        /// </summary>
        [FromQuery(Name = "encoding")]
        public Encoding Encoding { get; set; } = Encoding.Default;
    }
}