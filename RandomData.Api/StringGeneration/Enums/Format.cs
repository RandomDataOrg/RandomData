using System.Text.Json.Serialization;

namespace RandomData.Api.StringGeneration.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Format
    {
        Default,
        Upper,
        Lower,
        Pascal,
        Camel,
        Snake,
        Kebab
    }
}