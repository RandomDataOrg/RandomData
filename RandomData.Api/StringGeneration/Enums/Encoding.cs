using System.Text.Json.Serialization;

namespace RandomData.Api.StringGeneration.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Encoding
    {
        Default,
        Base64
    }
}