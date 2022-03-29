namespace EMS.Data.Entities
{
    public class Employee : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeId { get; set; }
        public string Gender { get; set; }
        public bool Enabled { get; set; } = true; 
        public string Designation { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal Salary { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public virtual Wallet Wallet { get; set; }
        public Guid DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}