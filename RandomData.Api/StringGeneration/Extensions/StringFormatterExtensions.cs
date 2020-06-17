using System.Linq;
using RandomData.Api.StringGeneration.Enums;

namespace RandomData.Api.StringGeneration.Extensions
{
    public static class StringFormatterExtensions
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
            return input.ToHashSet().SetEquals(new[] {' '}) ? string.Empty : input.ToLower().Replace(' ', '_');
        }

        public static string ToKebabCase(this string input)
        {
            return input.ToHashSet().SetEquals(new[] {' '}) ? string.Empty : input.ToLower().Replace(' ', '-');
        }
    }
}