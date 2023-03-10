using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public static class SolforbDbExtensions
    {
        #region Table Providers
        public static async Task<List<Provider>> GetProvidersAsync(this SolforbDbContext solforbDbContext)
        {
            return await solforbDbContext.Providers.ToListAsync();
        }
        #endregion
    }
}
