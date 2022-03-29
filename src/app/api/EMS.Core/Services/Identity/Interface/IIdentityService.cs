namespace EMS.Core.Services.Identity.Interface
{
    public interface IIdentityService : IAutoDependencyCore
    {
        string GetUserId();
    }
}