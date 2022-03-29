namespace EMS.Data.Models.Employee
{
    public class UpdatePasswordRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string CurrentPassword { get; set; }
    }
}