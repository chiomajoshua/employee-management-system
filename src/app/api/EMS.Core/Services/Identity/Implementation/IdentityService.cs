namespace EMS.Core.Services.Identity.Implementation
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x =>
                x.Type == JwtRegisteredClaimNames.Sid);

            return !string.IsNullOrEmpty(userId.Value) ? userId.Value : throw new Exception("User Id Not Found");
        }
    }
}