namespace EMS.Core
{
    public static class Extensions
    {
        public static IServiceCollection RegisterGenericRepository(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddGenericRepository<EMSContext>();
            return serviceCollection;
        }
    }
}