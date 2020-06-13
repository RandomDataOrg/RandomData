using System;
using RandomData.Api.Services.StringServices.Enums;

namespace RandomData.Api.Services.StringServices.ExtensionMethods
{
    public static class StringEncoderExtensionMethods
    {
        public static string EncodeTo(this string input, Encoding encoding) => encoding switch
        {
            Encoding.Base64 => input.ToBase64(),
            _ => input
        };
        
        public static string ToBase64(this string input)
        {
            return Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(input));
        }
    }
}