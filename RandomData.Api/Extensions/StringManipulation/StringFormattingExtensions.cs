using System.Linq;
using RandomData.Api.Extensions.StringManipulation.Enums;

namespace RandomData.Api.Extensions.StringManipulation
{
    public static class StringFormattingExtensions
    {
        public static string FormatTo(this string input, Format format)
        {
            return format switch
            {
                Format.Upper => input.ToUpper(),
                Format.Lower => input.ToLower(),
                Format.Pascal => input.ToPascalCase(),
                Format.Camel => input.ToCamelCase(),
                Format.Kebab => input.ToKebabCase(),
                Format.Snake => input.ToSnakeCase(),
                _ => input
            };
        }

        public static string ToPascalCase(this string input)
        {
            if (input == null)
                return null;
            var makeUpperCase = true;
            return new string(input
                .ToCharArray()
                .Select(x =>
                {
                    if (x == ' ')
                    {
                        makeUpperCase = true;
                        return x;
                    }

                    if (makeUpperCase)
                    {
                        makeUpperCase = false;
                        return char.ToUpper(x);
                    }

                    return char.ToLower(x);
                })
                .Where(x => x != ' ').ToArray());
        }

        public static string ToCamelCase(this string input)
        {
            if (input == null)
                return null;
            var makeUpperCase = false;
            return new string(input
                .ToCharArray()
                .Select(x =>
                {
                    if (x == ' ')
                    {
                        makeUpperCase = true;
                        return x;
                    }

                    if (makeUpperCase)
                    {
                        makeUpperCase = false;
                        return char.ToUpper(x);
                    }

                    return char.ToLower(x);
                })
                .Where(x => x != ' ').ToArray());
        }

        public static string ToSnakeCase(this string input)
        {
            return input?.Trim().ToLower().Replace(' ', '_');
        }

        public static string ToKebabCase(this string input)
        {
            return input?.Trim().ToLower().Replace(' ', '-');
        }
    }
}