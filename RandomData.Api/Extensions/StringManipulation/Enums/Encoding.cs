using System.Text.Json.Serialization;

namespace RandomData.Api.Extensions.StringManipulation.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Encoding
    {
        Default,
        Base64
    }
}