using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RandomData.Api.Controllers
{
	[ApiController]
	[Route("/guid")]
	public class GuidController
	{
		public async Task<string> GetRandomGuid()
		{
			return Guid.NewGuid().ToString();
		}
	}
}
