using System.Text.Json.Serialization;

namespace RandomData.Api.GuidGenerators
{
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum RandomGuidFormats
	{
		Default = 0,
		WithoutHyphens = 1,
		Uppercase = 2,
	}
}