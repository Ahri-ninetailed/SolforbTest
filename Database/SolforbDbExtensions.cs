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
        public static IEnumerable<int> GetProvidersId(this SolforbDbContext solforbDbContext)
        {
            return solforbDbContext.Providers.Select(p => p.Id).Distinct();
        }
        public static IEnumerable<string> GetProvidersName(this SolforbDbContext solforbDbContext)
        {
            return solforbDbContext.Providers.Select(p => p.Name).Distinct();
        }
        #endregion

        #region Table Orders
        public static IEnumerable<string> GetDistinctOrderNumbers(this SolforbDbContext solforbDbContext)
        {
            return solforbDbContext.Orders.Select(o => o.Number).Distinct();
        }
        public static async Task<int> GetOrderIdByOrderItemId(this SolforbDbContext solforbDbContext, int orderItemId)
        {
            OrderItem orderItem = await solforbDbContext.GetOrderItemByIdAsync(orderItemId);
            return orderItem.OrderId;
        }
        public static async Task DeleteOrderByIdAsync(this SolforbDbContext solforbDbContext, int id)
        {
            Order order = await solforbDbContext.GetOrderByIdAsync(id);
            solforbDbContext.Orders.Remove(order);
            await solforbDbContext.SaveChangesAsync();
        }
        public static async Task CreateOrderAsync(this SolforbDbContext solforbDbContext, Order order)
        {
            await solforbDbContext.Orders.AddAsync(order);
            await solforbDbContext.SaveChangesAsync();
        }
        public static async Task<Order> GetOrderByProviderAndNumberAsync(this SolforbDbContext solforbDbContext, int providerId, string orderNumber)
        {
            return await solforbDbContext.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.ProviderId == providerId && o.Number == orderNumber);
        }
        public static async Task<Order> GetOrderByIdAsync(this SolforbDbContext solforbDbContext, int Id)
        {
            return await solforbDbContext.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == Id);
        }
        public static async Task UpdateOrderAsync(this SolforbDbContext solforbDbContext, Order order)
        {
            solforbDbContext.Orders.Update(order);
            await solforbDbContext.SaveChangesAsync();
        }
        public static async Task<IEnumerable<Order>> GetOrdersAsync(this SolforbDbContext solforbDbContext)
        {
            return await solforbDbContext.Orders.Include(o => o.OrderItems).ToArrayAsync();
        }
        #endregion
        #region Table OrderItems
        public static IEnumerable<string> GetDistinctItemNames(this SolforbDbContext solforbDbContext)
        {
            return solforbDbContext.OrderItems.Select(ot => ot.Name).Distinct();
        }
        public static IEnumerable<decimal> GetDistinctItemQuantities(this SolforbDbContext solforbDbContext)
        {
            return solforbDbContext.OrderItems.Select(ot => ot.Quantity).Distinct();
        }
        public static IEnumerable<string> GetDistinctItemUnits(this SolforbDbContext solforbDbContext)
        {
            return solforbDbContext.OrderItems.Select(ot => ot.Unit).Distinct();
        }
        public static async Task UpdateOrderItemAsync(this SolforbDbContext solforbDbContext, OrderItem orderItem)
        {
            solforbDbContext.OrderItems.Update(orderItem);
            await solforbDbContext.SaveChangesAsync();
        }
        public static async Task CreateOrderItemAsync(this SolforbDbContext solforbDbContext, OrderItem orderItem)
        {
            await solforbDbContext.OrderItems.AddAsync(orderItem);
            await solforbDbContext.SaveChangesAsync();
        }
        public static async Task<OrderItem> GetOrderItemByIdAsync(this SolforbDbContext solforbDbContext, int id)
        {
            return await solforbDbContext.OrderItems.FirstOrDefaultAsync(o => o.Id == id);
        }
        public static async Task DeleteOderItemByIdAsync(this SolforbDbContext solforbDbContext, int id)
        {
            var orderItem = await solforbDbContext.GetOrderItemByIdAsync(id);
            var order = await solforbDbContext.GetOrderByIdAsync(orderItem.OrderId);
            order.OrderItems.Remove(orderItem);
            await solforbDbContext.UpdateOrderAsync(order);
        }
        #endregion
    }
}
