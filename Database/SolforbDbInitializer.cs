using Database.Models;

namespace Database
{
    public static class SolforbDbInitializer
    {
        /// <summary>
        /// Инициализирует бд начальными значениями
        /// </summary>
        /// <param name="context"></param>
        public static void Initialize(SolforbDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Providers.Any())
                return;

            var providers = new Provider[]
            {
                new Provider { Name = "RedTable" },
                new Provider { Name = "BlackWater" },
            };
            foreach (var provider in providers)
                context.Providers.Add(provider);
            context.SaveChanges();
        }
    }
}
