namespace EMS.Data.Models.Employee
{
    public class UpdatePasswordRequest
    {
        public string Password { get; set; }
        public string CurrentPassword { get; set; }
    }
}