namespace EMS.Data.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }

        //Other things that can be added in future:
        //Head of Department
        //Possible Size of Department
        //DepartmentDescription
        //BusinessArmAttachedTo, etc
    }
}