using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using TransportPractice.Contracts;

namespace TransportPractice.Controllers
{
	[Route("[controller]/[action]")]
	public class HomeController : ControllerBase
	{
		private readonly IRequestClient<CreateCustomerRequest> _createCustomerRequestClient;

		public HomeController(IRequestClient<CreateCustomerRequest> createCustomerRequestClient)
		{
			_createCustomerRequestClient = createCustomerRequestClient;
		}

		[HttpPost]
		public async Task<IActionResult> CreateCustomer(CreateCustomerRequest request)
		{
			var response = await _createCustomerRequestClient.GetResponse<CreateCustomerResponse>(request);

			return Ok(response.Message);
		}
	}
}
