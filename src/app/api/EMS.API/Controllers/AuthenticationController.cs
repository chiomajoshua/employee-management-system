namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IIdentityService _identityService;
        public AuthenticationController(IEmployeeService employeeService, IIdentityService identityService)
        {
            _employeeService = employeeService;
            _identityService = identityService;
        }

        [HttpPost, Route("login")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post(LoginRequest loginRequest)
        {
            if (!await _employeeService.IsEmployeeExistsAsync(loginRequest.Email))
                return NotFound("Employee Information Does Not Exist");          

            if (!await _employeeService.IsAuthorizedAsync(loginRequest.Email)) 
                return Unauthorized("Account is locked!. Please contact administrator");

            if (await _identityService.Login(loginRequest)) 
                return Ok(await _identityService.CreateToken(loginRequest.Email));

            return Unauthorized("Invalid login credentials");
        }
    }
}