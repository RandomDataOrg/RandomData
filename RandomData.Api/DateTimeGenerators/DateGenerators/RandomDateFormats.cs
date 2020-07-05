using System.Text.Json.Serialization;

namespace RandomData.Api.DateTimeGenerators.DateGenerators
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RandomDateFormats
    {
        Short = 0,
        Long = 1,
        MonthDay = 2,
        YearMonth = 3
    }
}
