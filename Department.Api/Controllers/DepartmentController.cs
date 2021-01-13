using System.Threading.Tasks;
using Department.Contracts.CreateDepartment;
using Department.Contracts.GetAllDepartments;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Department.Api.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class DepartmentController : ControllerBase
	{
		private readonly IRequestClient<CreateDepartmentRequest> _createDepartmentRequestClient;
		private readonly IRequestClient<GetAllDepartmentRequest> _getAllDepartmentRequestClient;

		public DepartmentController(
			IRequestClient<CreateDepartmentRequest> createDepartmentRequestClient, 
			IRequestClient<GetAllDepartmentRequest> getAllDepartmentRequestClient)
		{
			_createDepartmentRequestClient = createDepartmentRequestClient;
			_getAllDepartmentRequestClient = getAllDepartmentRequestClient;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllDepartment()
		{
			var response =
				await _getAllDepartmentRequestClient.GetResponse<GetAllDepartmentResponse>(new GetAllDepartmentRequest());
			return Ok(response.Message.Departments);
		}

		[HttpPost]
		public async Task<IActionResult> CreateDepartment(CreateDepartmentRequest request)
		{
			if (ModelState.IsValid)
			{
				var response = await _createDepartmentRequestClient.GetResponse<CreateDepartmentResponse>(request);
				return Accepted(response.Message);
			}

			return BadRequest(ModelState);
		}
	}
}