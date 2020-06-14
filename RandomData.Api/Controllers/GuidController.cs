using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RandomData.Api.Generators;

namespace RandomData.Api.Controllers
{
	[ApiController]
	[Route("/guid")]
	public class GuidController
	{
		private readonly IGuidGenerator _guidGenerator;

		public GuidController(IGuidGenerator guidGenerator)
		{
			_guidGenerator = guidGenerator;
		}

		public async Task<string> GetRandomGuid()
		{
			return await _guidGenerator.GetRandom();
		}
	}
}
