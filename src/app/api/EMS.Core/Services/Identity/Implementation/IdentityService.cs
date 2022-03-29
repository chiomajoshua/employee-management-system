namespace EMS.Core.Services.Identity.Implementation
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<Employee> _userManager;
        private readonly IConfiguration _configuration;
        public IdentityService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<TokenResponse> CreateToken(string emailAddress)
        {
            var employeeDetails = await _userManager.FindByEmailAsync(emailAddress);
            var userRoles = await _userManager.GetRolesAsync(employeeDetails);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, employeeDetails.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, employeeDetails.Id),
                };

            authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT:Secret")));

            var token = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("JWT:ValidIssuer"),
                audience: _configuration.GetValue<string>("JWT:ValidAudience"),
                expires: DateTime.Now.AddHours(_configuration.GetValue<int>("JWT:Validity")),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new TokenResponse { Token = new JwtSecurityTokenHandler().WriteToken(token), Expires = token.ValidTo };
          
        }

        public string GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x =>
                x.Type == JwtRegisteredClaimNames.Sid);
            return !string.IsNullOrEmpty(userId.Value) ? userId.Value : throw new Exception("User Id Not Found");
        }

        public async Task<bool> Login(LoginRequest loginRequest) => 
             await _userManager.CheckPasswordAsync(await _userManager.FindByEmailAsync(loginRequest.Email), loginRequest.Password);
    }
}