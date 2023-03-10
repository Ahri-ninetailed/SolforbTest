using Database;

namespace SolforbTest.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void CreateDbIfNotExists(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<SolforbDbContext>();
                    SolforbDbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при создании БД:\n{ex.Message}");
                }
            }
        }
    }
}
