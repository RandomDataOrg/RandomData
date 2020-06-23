using System;

namespace RandomData.Api.GuidGenerators
{
	public class GuidGenerator : IGuidGenerator
	{
		public string GetRandom(GetRandomGuidParameters parameters)
		{
			var guid = Guid.NewGuid();
			var formattedGuid = guid.ToString();
			foreach (var format in parameters.Formats)
				formattedGuid = GetFormattedGuid(formattedGuid, format);
			return formattedGuid;
		}

		private string GetFormattedGuid(string formattedGuid, RandomGuidFormats format) =>
			format switch
			{
				RandomGuidFormats.Default => formattedGuid,
				RandomGuidFormats.WithoutHyphens => formattedGuid.Replace("-", ""),
				RandomGuidFormats.Uppercase => formattedGuid.ToUpper(),
				_ => throw new ArgumentOutOfRangeException(nameof(format), format, null)
			};
	}
}