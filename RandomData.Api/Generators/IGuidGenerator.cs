using System;
using System.Threading.Tasks;

namespace RandomData.Api.Generators
{
	public interface IGuidGenerator
	{
		Task<string> GetRandom();
	}

	public class GuidGenerator : IGuidGenerator
	{
		public async Task<string> GetRandom()
		{
			return Guid.NewGuid().ToString();
		}
	}
}
