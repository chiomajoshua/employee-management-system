namespace EMS.Data.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection RegisterDatabaseService(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString)) throw new Exception("Connection String Is Not Set");
            serviceCollection.AddDbContext<EMSContext>(options => options.UseSqlServer(connectionString));
            return serviceCollection;
        }
    }
}