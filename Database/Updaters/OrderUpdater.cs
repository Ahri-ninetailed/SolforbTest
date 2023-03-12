using Database.Models;

namespace Database.Updaters
{
    public static class OrderUpdater
    {
        public static Order SetNumber(this Order order, string newNumber)
        {
            order.Number = newNumber;
            return order;
        }
        public static Order SetProviderId(this Order order, int newProviderId)
        {
            order.ProviderId = newProviderId;
            return order;
        }
        public static Order SetDate(this Order order, DateTime newDate)
        {
            order.Date = newDate;
            return order;
        }
        public static void UpdateOrder(this Order order, Order newOrder)
        {
            order
                .SetDate(newOrder.Date)
                .SetProviderId(newOrder.ProviderId)
                .SetNumber(newOrder.Number);
        }
    }
}
