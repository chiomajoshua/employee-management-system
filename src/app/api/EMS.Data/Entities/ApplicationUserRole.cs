namespace EMS.Data.Entities
{
    public class ApplicationUserRole : IdentityUserRole<Guid>
    {
        public virtual Employee Employee { get; set; }
        public virtual ApplicationUserRole Role { get; set; }
    }
}