namespace EMS.Core.Services.ApplicationUser.Config
{
    public static class UserExtensions
    {
        public static EmployeeResponse ToEmployee(this Employee employee, IEnumerable<string> roles)
        {
            return new EmployeeResponse
            {
                IsBlocked = employee.Enabled,
                Email = employee.Email,
                FirstName = employee.FirstName,
                Phone = employee.PhoneNumber,
                LastName = employee.LastName,
                Name = employee.ToString(),
                Gender = employee.Gender,
                DateOfBirth = employee.DateOfBirth,
                Id = employee.Id,
                Roles = roles
            };
        }

        public static Employee ToDbEmployee(this CreateEmployeeRequest createEmployeeRequest)
        {
            return new Employee
            {
                Email = createEmployeeRequest.Email,
                FirstName = createEmployeeRequest.FirstName,
                LastName = createEmployeeRequest.LastName,
                PhoneNumber = createEmployeeRequest.PhoneNumber,
                UserName = createEmployeeRequest.Email,
                DateOfBirth = createEmployeeRequest.DateOfBirth,
                Gender = createEmployeeRequest.Gender,
                Enabled = true,
                EmployeeId = $"EMP-{DateTime.Now.Ticks}",
                Designation = createEmployeeRequest.Designation,
                Salary = createEmployeeRequest.Salary,
                DepartmentId = createEmployeeRequest.DepartmentId
            };
        }
    }
}