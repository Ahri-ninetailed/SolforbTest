using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public static class OrderExtensions
    {
        public static IEnumerable<Order> OrdersByDates(this IEnumerable<Order> orders, DateTime firstDate, DateTime secondDate)
        {
            
            return orders.Where(o => o.Date >= firstDate && o.Date <= secondDate);
        }
        public static IEnumerable<Order> OrdersByProvidersId(this IEnumerable<Order> orders, IEnumerable<int> providersId)
        {
            return providersId is not null ? orders.Where(o => providersId.Any(p => p == o.ProviderId)) : orders;
        }
        public static IEnumerable<Order> OrdersByNumbers(this IEnumerable<Order> orders, IEnumerable<string> ordersNumbers)
        {
            return ordersNumbers is not null ? orders.Where(o => ordersNumbers.Any(on => on == o.Number)) : orders;
        }
        public static IEnumerable<Order> OrdersByItemsNames(this IEnumerable<Order> orders, IEnumerable<string> itemsNames) 
        {
            return itemsNames is not null ? orders.Where(o => o.OrderItems.Any(ot => itemsNames.Any(it => it == ot.Name))) : orders;
        }
        public static IEnumerable<Order> OrdersByItemsQuantities(this IEnumerable<Order> orders, IEnumerable<decimal> itemsQuantities)
        {
            return itemsQuantities is not null ? orders.Where(o => o.OrderItems.Any(ot => itemsQuantities.Any(it => it == ot.Quantity))) : orders;
        }
        public static IEnumerable<Order> OrdersByItemsUnits(this IEnumerable<Order> orders, IEnumerable<string> itemsUnits)
        {
            return itemsUnits is not null ? orders.Where(o => o.OrderItems.Any(ot => itemsUnits.Any(it => it == ot.Unit))) : orders;
        }
        public static IEnumerable<Order> OrdersByProvidersNames(this IEnumerable<Order> orders, IEnumerable<string> providersNames, IEnumerable<Provider> providers)
        {
            providers = providers.Where(p => providersNames.Any(pn => pn == p.Name));
            return providersNames is not null ? orders.Where(o => providers.Any(p => p.Id == o.ProviderId)) : orders;
        }
    }
}
