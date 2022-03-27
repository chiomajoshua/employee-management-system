using System.IdentityModel.Tokens.Jwt;

namespace EMS.Data.Persistence
{
    public class EMSContext : IdentityDbContext<Employee>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EMSContext(DbContextOptions<EMSContext> dbContextOptions, IHttpContextAccessor httpContextAccessor) : base(dbContextOptions) 
        { 
            _httpContextAccessor = httpContextAccessor;
        }

        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Wallet> Wallet { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Wallet>()
                        .HasOne(ad => ad.Employee)
                        .WithOne(s => s.Wallet)
                        .HasForeignKey<Wallet>(ad => ad.Employee.Id);

        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            if (!entries.Any()) return;

            var userId = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x =>
                x.Type == JwtRegisteredClaimNames.Sid);

            var now = DateTime.Now;

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).CreatedAt = now;
                    ((BaseEntity)entry.Entity).CreatedById = Guid.Parse(userId?.Value);
                }
                ((BaseEntity)entry.Entity).UpdatedAt = now;
                ((BaseEntity)entry.Entity).UpdatedById = Guid.Parse(userId?.Value);
            }
        }
    }
}