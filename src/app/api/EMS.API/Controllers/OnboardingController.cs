namespace EMS.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class OnboardingController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public OnboardingController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [Authorize(Roles = ApplicationUserRoleName.AdminRoleName)]
        [HttpPost, Route("onboardemployee")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Post(CreateEmployeeRequest createEmployeeRequest)
        {
            var employeeResult = await _employeeService.IsEmployeeExistsAsync(createEmployeeRequest.Email);
            if (employeeResult) return Conflict("Employee Email Already Registered");

            if (await _employeeService.CreateEmployeeAsync(createEmployeeRequest)) return NoContent();

            return Problem();
        }

        [Authorize(Roles = ApplicationUserRoleName.EmployeeRoleName)]
        [HttpPut, Route("updateemployee")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(UpdateEmployeeRequest updateEmployeeRequest)
        {
            var employeeResult = await _employeeService.IsEmployeeExistsAsync(updateEmployeeRequest.EmailAddress);
            if (!employeeResult) return Conflict("Employee Information Not Found");

            if (await _employeeService.UpdateEmployeeInformation(updateEmployeeRequest)) return Ok();

            return Problem();
        }
    }
}