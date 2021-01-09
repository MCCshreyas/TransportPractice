using System.Threading.Tasks;
using Employee.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Api.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class EmployeeController : ControllerBase
	{
		private readonly IRequestClient<CreateEmployeeRequest> _requestClient;

		public EmployeeController(IRequestClient<CreateEmployeeRequest> requestClient)
		{
			_requestClient = requestClient;
		}

		[HttpPost]
		public async Task<IActionResult> CreateEmployee(CreateEmployeeRequest request)
		{
			if (ModelState.IsValid)
			{
				var response = await _requestClient.GetResponse<CreateEmployeeResponse>(request);
				return Ok(response.Message);
			}

			return BadRequest(ModelState);
		}
	}
}