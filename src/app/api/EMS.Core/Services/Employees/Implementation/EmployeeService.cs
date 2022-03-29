namespace EMS.Core.Services.ApplicationUser.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IRepository _repository;
        private readonly IIdentityService _identityService;
        
        public EmployeeService(UserManager<Employee> userManager, IRepository repository, IIdentityService identityService)
        {
            _userManager = userManager;
            _repository = repository;
            _identityService = identityService;
        }

        public async Task<bool> ChangePasswordAsync(string userName, UpdatePasswordRequest updateUserRequest)
        {
            var user = await GetByUserNameAndThrow(userName);
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, updateUserRequest.CurrentPassword, updateUserRequest.Password);
                if (!result.Succeeded)
                    throw new Exception("Error processing password change");
                return true;
            }
            throw new Exception("Employee Not Found");
        }

        public async Task<bool> CreateEmployeeAsync(CreateEmployeeRequest createEmployeeRequest)
        {
            var userId = Guid.Parse(_identityService.GetUserId());
            var employeeRequest = createEmployeeRequest.ToDbEmployee();
            var result = await _userManager.CreateAsync(employeeRequest, createEmployeeRequest.Password);
            if (!result.Succeeded)
            {
                if (result.Errors.Any(x => x.Code == "DuplicateUserName"))
                    throw new Exception("DuplicateUserName");
                throw new Exception(result.Errors.FirstOrDefault().Description);
            }     

            if (createEmployeeRequest.IsAdmin)
                await _userManager.AddToRoleAsync(employeeRequest, ApplicationUserRoleName.AdminRoleName);
            else
                await _userManager.AddToRoleAsync(employeeRequest, ApplicationUserRoleName.EmployeeRoleName);

            var userProfile = await GetByUserNameAndThrow(createEmployeeRequest.Email);
            await _repository.InsertAsync(new Wallet { Balance = 0, CreatedById = userId, UpdatedById = userId, EmployeeId = userProfile.Id, Employee = userProfile });

            return true;
        }

        public async Task<Employee> FindDbEmployeeByEmailAsync(string emailAddress)
        {
            if (string.IsNullOrWhiteSpace(emailAddress)) throw new Exception("Employee Not Found");
            var isUserExist = await _userManager.FindByEmailAsync(emailAddress);
            if (isUserExist != null)
                return isUserExist;            
            return null;
        }

        public async Task<EmployeeResponse> FindEmployeeByEmailAsync(string emailAddress)
        {
            if (string.IsNullOrWhiteSpace(emailAddress)) throw new Exception("Employee Not Found");
            var isUserExist = await _userManager.FindByEmailAsync(emailAddress);
            if (isUserExist != null)
                return isUserExist.ToEmployee(await GetEmployeeRole(isUserExist));
            return null;
        }

        public async Task<bool> IsAuthorizedAsync(string userName)
        {
            if (await _userManager.FindByNameAsync(userName) == null) return false;
            return (await _userManager.FindByNameAsync(userName)).Enabled;
        }

        public async Task<bool> LockAsync(string userName) => await SetEnabled(userName, false);

        public async Task<bool> UnlockAsync(string userName) => await SetEnabled(userName, true);        

        public async Task<Employee> FindDbEmployeeByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new Exception("Employee Not Found");
            return await _userManager.FindByIdAsync(id) ?? null;
        }

        public async Task<bool> IsEmployeeExistsAsync(string emailAddress)
        {
            return await _repository.ExistsAsync<Employee>(x => x.Email == emailAddress);
        }


        private async Task<bool> SetEnabled(string userName, bool enabled)
        {
            var user = await GetByUserNameAndThrow(userName);
            user.Enabled = enabled;
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        private async Task<Employee> GetByUserNameAndThrow(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName)) throw new Exception("Employee Not Found");

            var result = _userManager.Users.Include(x => x.Wallet).Include(x => x.Department)
                                           .FirstOrDefault(x => x.UserName == userName);
            return result == null ? throw new Exception("Employee Not Found") : await Task.FromResult(result);
        }

        private async Task<IEnumerable<string>> GetEmployeeRole(Employee employee) => await _userManager.GetRolesAsync(employee);
    }
}