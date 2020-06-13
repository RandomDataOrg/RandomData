using System.Text.Json.Serialization;

namespace RandomData.Api.Services.StringServices.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum GenerationTypes
    {
        Random,
        Words
    }
}