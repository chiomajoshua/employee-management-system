namespace EMS.Core.Services.ApplicationUser.Interface
{
    public interface IEmployeeService : IAutoDependencyCore
    {
        /// <summary>
        /// Create a new employee account
        /// </summary>
        /// <param name="createEmployeeRequest"></param>
        /// <returns></returns>
        Task<bool> CreateEmployeeAsync(CreateEmployeeRequest createEmployeeRequest);

        /// <summary>
        /// Find employee by email 
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        Task<EmployeeResponse> FindEmployeeByEmailAsync(string emailAddress);

        /// <summary>
        /// Check if the employee is authorized to access this application
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<bool> IsAuthorizedAsync(string userName);

        /// <summary>
        /// Locks employee access
        /// </summary>
        /// <param name="userId">the user id to lock</param>
        /// <returns></returns>
        Task<bool> LockAsync(string userName);

        /// <summary>
        /// Unlock employee access
        /// </summary>
        /// <param name="userId">the user id to unlock</param>
        /// <returns></returns>
        Task<bool> UnlockAsync(string userName);

        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="updateUserRequest"></param>
        /// <returns></returns>
        Task<bool> ChangePasswordAsync(string userName, UpdatePasswordRequest updateUserRequest);

        /// <summary>
        /// Find Db employee by email / username 
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        Task<Employee> FindDbEmployeeByEmailAsync(string emailAddress);

        /// <summary>
        /// Find Db employee by Id
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        Task<Employee> FindDbEmployeeByIdAsync(string id);

        /// <summary>
        /// Check If Employee Email Is Already In The Database
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        Task<bool> IsEmployeeExistsAsync(string emailAddress);

        /// <summary>
        /// Get All Employees
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<IEnumerable<Employee>> GetEmployees(int skip, int take);
    }
}