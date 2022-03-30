namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnquiriesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EnquiriesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [Authorize(Roles = $"{ApplicationUserRoleName.AdminRoleName},{ApplicationUserRoleName.EmployeeRoleName}")]
        [HttpGet, Route("getemployeeInformation")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string emailAddress)
        {
            if (!await _employeeService.IsEmployeeExistsAsync(emailAddress))
                return NotFound("Employee Information Does Not Exist");

            return Ok(await _employeeService.FindEmployeeByEmailAsync(emailAddress));
        }

        [Authorize(Roles = $"{ApplicationUserRoleName.AdminRoleName}")]
        [HttpPost, Route("getemployees")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(PagedRequest pagedRequest) => Ok(await _employeeService.GetEmployees(pagedRequest.Skip, pagedRequest.Take));
    }
}