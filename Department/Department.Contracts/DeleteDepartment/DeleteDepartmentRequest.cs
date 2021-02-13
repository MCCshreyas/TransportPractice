namespace Department.Contracts.DeleteDepartment
{
	public record DeleteDepartmentRequest
	{
		public int DepartmentId { get; set; }
	}

	public record DeleteDepartmentResponse
	{
		public int DepartmentId { get; set; }
		public bool IsDeleted { get; set; }
	}
}