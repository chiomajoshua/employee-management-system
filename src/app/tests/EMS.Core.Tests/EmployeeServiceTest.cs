

using EMS.Data.Models.Employee;
using FluentAssertions;
using System;

namespace EMS.Core.Tests
{
    public class EmployeeServiceTest
    {
        private readonly Mock<UserManager<Employee>> _userManager;
        private readonly Mock<RoleManager<IdentityRole>> _roleManager;
        private Mock<IRepository> _repositoryService;
        private readonly Mock<IIdentityService> _identityService;
        private readonly Mock<IWalletService> _walletService;

        private readonly EmployeeService _employeeService;
        public EmployeeServiceTest()
        {
            _userManager = new Mock<UserManager<Employee>>();
            _roleManager = new Mock<RoleManager<IdentityRole>>();
            _repositoryService = new Mock<IRepository>();
            _identityService = new Mock<IIdentityService>();
            _walletService = new Mock<IWalletService>();

            _employeeService = new EmployeeService(_userManager.Object, _roleManager.Object, _repositoryService.Object, _identityService.Object,
                _walletService.Object);
        }

        [Fact(DisplayName = "Should Return true after changing password")]
        public async Task EmployeeService_ChangePasswordAsync_Success()
        {
            var username = "tester@gmail.com";
            var updatePasswordRequest = new UpdatePasswordRequest()
            {
                 CurrentPassword = "Password#007",
                 Password = "Password007#"
            };

            var employee = new Employee();
            _userManager.Setup(ap => ap.FindByEmailAsync(username)).ReturnsAsync(employee);

            var response = await _employeeService.ChangePasswordAsync(username, updatePasswordRequest);
            response.Should().Be(true);
        }

        [Fact(DisplayName = "Should Return false")]
        public async Task EmployeeService_ChangePasswordAsync_Failed()
        {
            var username = "tester@gmail.com";
            var updatePasswordRequest = new UpdatePasswordRequest()
            {
                CurrentPassword = "Passwor22d007",
                Password = "Password007#"
            };

            var employee = new Employee();
            _userManager.Setup(ap => ap.FindByEmailAsync(username)).ReturnsAsync(employee);

            var response = await _employeeService.ChangePasswordAsync(username, updatePasswordRequest);

            response.Should().Be(false);
            
        }




    }
}