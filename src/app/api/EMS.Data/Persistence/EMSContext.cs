using System.IdentityModel.Tokens.Jwt;

namespace EMS.Data.Persistence
{
    public class EMSContext : IdentityDbContext<Employee>
    {
        public EMSContext(DbContextOptions<EMSContext> dbContextOptions) : base(dbContextOptions) 
        { 
            
        }

        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Wallet> Wallet { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Wallet>()
                        .HasOne(ad => ad.Employee)
                        .WithOne(s => s.Wallet)
                        .HasForeignKey<Wallet>(ad => ad.EmployeeId);

        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {            
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}