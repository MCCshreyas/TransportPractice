using System.Collections.Generic;
using System.Threading.Tasks;
using Employee.Contracts;
using Employee.Contracts.CreateEmployee;
using Employee.Contracts.GetAllEmployee;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Api.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class EmployeeController : ControllerBase
	{
		private readonly IRequestClient<CreateEmployeeRequest> _requestClient;
		private readonly IRequestClient<GetAllEmployeeRequest> _getAllEmployeeRequestClient;

		public EmployeeController(
			IRequestClient<CreateEmployeeRequest> createEmployeeRequestClient,
			IRequestClient<GetAllEmployeeRequest> getAllEmployeeRequestClient)
		{
			_requestClient = createEmployeeRequestClient;
			_getAllEmployeeRequestClient = getAllEmployeeRequestClient;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllEmployees()
		{
			var response =
				await _getAllEmployeeRequestClient.GetResponse<GetAllEmployeeResponse>(new GetAllEmployeeRequest());

			return Ok(response.Message.Employee);
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