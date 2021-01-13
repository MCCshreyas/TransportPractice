
dotnet ef migrations add "EmployeeFirst" --project .\Employee.Infrastructure\ --startup-project .\Employee.Service\Employee.Service.csproj

dotnet ef database update  --project .\Employee.Infrastructure\ --startup-project .\Employee.Service\Employee.Service.csproj

dotnet ef migrations add "DepartmentFirst" --project .\Department.Infrastructure\ --startup-project .\Department.Service\Department.Service.csproj

dotnet ef database update  --project .\Department.Infrastructure\ --startup-project .\Department.Service\Department.Service.csproj