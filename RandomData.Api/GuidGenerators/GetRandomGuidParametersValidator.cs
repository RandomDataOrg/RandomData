using FluentValidation;
using System.Linq;

namespace RandomData.Api.GuidGenerators
{
	public class GetRandomGuidParametersValidator : AbstractValidator<GetRandomGuidParameters>, IGetRandomGuidParametersValidator
	{
		public const string ErrorCode = "invalid_random_guid_generator_parameters";

		public GetRandomGuidParametersValidator()
		{
			RuleFor(x => x.Formats)
				.Must(format => format.Length == 1)
				.When(x => x.Formats.Contains(RandomGuidFormats.Default))
				.WithErrorCode(ErrorCode)
				.WithMessage($"{nameof(RandomGuidFormats.Default)} format cannot be combined with any other format.");
		}
	}
}