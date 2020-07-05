using System.Text.Json.Serialization;

namespace RandomData.Api.DateTimeGenerators.TimeGenerators
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RandomTimeFormats
    {
        Short = 0,
        Long = 1
    }
}
