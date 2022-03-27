namespace EMS.Data.Models.Onboarding
{
    public class CreateEmployeeRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public Guid DepartmentId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}