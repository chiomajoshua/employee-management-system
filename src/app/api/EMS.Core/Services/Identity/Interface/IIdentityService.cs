namespace EMS.Core.Services.Identity.Interface
{
    public interface IIdentityService : IAutoDependencyCore
    {
        string GetUserId();
        Task<bool> Login(LoginRequest loginRequest);
        Task<TokenResponse> CreateToken(string emailAddress);
    }
}