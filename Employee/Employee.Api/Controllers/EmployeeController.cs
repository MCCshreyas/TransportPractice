using Employee.Contracts.CreateEmployee;
using Employee.Contracts.EditEmployee;
using Employee.Contracts.GetAllEmployee;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Employee.Api.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class EmployeeController : ControllerBase
	{
		private readonly IRequestClient<CreateEmployeeRequest> _createEmployeeRequestClient;
		private readonly IRequestClient<GetAllEmployeeRequest> _getAllEmployeeRequestClient;
		private readonly IRequestClient<EditEmployeeRequest> _editEmployeeRequestClient;

		public EmployeeController(
			IRequestClient<CreateEmployeeRequest> createEmployeeRequestClient,
			IRequestClient<GetAllEmployeeRequest> getAllEmployeeRequestClient, IRequestClient<EditEmployeeRequest> editEmployeeRequestClient)
		{
			_createEmployeeRequestClient = createEmployeeRequestClient;
			_getAllEmployeeRequestClient = getAllEmployeeRequestClient;
			_editEmployeeRequestClient = editEmployeeRequestClient;
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
				var response = await _createEmployeeRequestClient.GetResponse<CreateEmployeeResponse>(request);
				return Ok(response.Message);
			}

			return BadRequest(ModelState);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> EditEmployee(int id, EditEmployeeRequest request)
		{
			if (ModelState.IsValid && id == request.Id)
			{
				var response =
					await _editEmployeeRequestClient.GetResponse<EditEmployeeResponse>(new EditEmployeeRequest
					{ Id = id, Name = request.Name });

				return Ok(response.Message);
			}

			return BadRequest(ModelState);
		}
	}
}