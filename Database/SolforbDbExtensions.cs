using Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;

namespace Database
{
    public static class SolforbDbExtensions
    {
        #region Table Providers
        public static async Task<List<Provider>> GetProvidersAsync(this SolforbDbContext solforbDbContext)
        {
            return await solforbDbContext.Providers.ToListAsync();
        }
        public static async Task<Provider> GetProviderByIdAsync(this SolforbDbContext solforbDbContext, int id)
        {
            return await solforbDbContext.Providers.FirstOrDefaultAsync(p => p.Id == id);
        }
        #endregion

        #region Table Orders
        public static async Task CreateOrderAsync(this SolforbDbContext solforbDbContext, Order order)
        {
            await solforbDbContext.Orders.AddAsync(order);
            await solforbDbContext.SaveChangesAsync();
        }
        public static async Task<Order> GetOrderByProviderAndNumberAsync(this SolforbDbContext solforbDbContext, int providerId, string orderNumber)
        {
            return await solforbDbContext.Orders.FirstOrDefaultAsync(o => o.ProviderId == providerId && o.Number == orderNumber);
        }
        public static async Task UpdateOrderAsync(this SolforbDbContext solforbDbContext, Order order)
        {
            solforbDbContext.Orders.Update(order);
            await solforbDbContext.SaveChangesAsync();
        }
        #endregion
        #region Table OrderItems
        public static async Task CreateOrderItemAsync(this SolforbDbContext solforbDbContext, OrderItem orderItem)
        {
            await solforbDbContext.OrderItems.AddAsync(orderItem);
            await solforbDbContext.SaveChangesAsync();
        }
        #endregion
    }
}
