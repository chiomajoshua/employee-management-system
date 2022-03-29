namespace EMS.Data.Models.Employee
{
    public class EmployeeResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeId { get; set; }
        public string Gender { get; set; }
        public bool IsActive { get; set; }
        public string Designation { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal Salary { get; set; }
        public decimal Balance { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsBlocked { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}