namespace EMS.API.Controllers
{
    [Authorize(Roles = ApplicationUserRoleName.AdminRoleName)]
    [Route("api/[controller]")]
    [ApiController]
    public class OnboardingController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public OnboardingController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

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
    }
}