namespace EMS.Data.Constants
{
    public class ApplicationUserRoleName
    {
        public const string AdminRoleName = "Administrator";
        public const string EmployeeRoleName = "Employee";

        public static readonly ApplicationUserRoleName Administrator = new(AdminRoleName);
        public static readonly ApplicationUserRoleName Employee = new(EmployeeRoleName);
        public readonly string Name;

        private ApplicationUserRoleName(string name)
        {
            Name = name;
        }

        public static string[] GetAllRoles()
        {
            return new[]
            {
                Administrator.Name,
                Employee.Name
            };
        }
    }
}