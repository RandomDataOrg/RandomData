using System;
using RandomData.Api.Extensions.StringManipulation.Enums;

namespace RandomData.Api.Extensions.StringManipulation
{
    public static class StringEncodingExtensions
    {
        public static string EncodeTo(this string input, Encoding encoding)
        {
            return encoding switch
            {
                Encoding.Base64 => input.ToBase64(),
                _ => input
            };
        }

        public static string ToBase64(this string input)
        {
            if (input == null)
                return null;
            return Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(input));
        }
    }
}