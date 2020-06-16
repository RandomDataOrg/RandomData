namespace RandomData.Api.GuidGenerators
{
	public interface IGuidGenerator
	{
		string GetRandom(GetRandomGuidParameters parameters);
	}
}