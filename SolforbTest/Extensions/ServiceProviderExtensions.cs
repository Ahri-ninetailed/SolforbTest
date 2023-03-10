using Database;

namespace SolforbTest.Extensions
{
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// Подключает базу данных
        /// </summary>
        /// <param name="services"></param>
        public static void AddDatabaseModule(this IServiceCollection services)
        {
            services.AddDbContext<SolforbDbContext>();
        }
    }
}
