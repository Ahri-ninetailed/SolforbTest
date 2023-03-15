using Database;
using System.Reflection;
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
        public static void AddMediatRModule(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}
