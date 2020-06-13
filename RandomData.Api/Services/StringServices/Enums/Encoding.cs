using System.Text.Json.Serialization;

namespace RandomData.Api.Services.StringServices.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Encoding
    {
        None,
        Base64
    }
}