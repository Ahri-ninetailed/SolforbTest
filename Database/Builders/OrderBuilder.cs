using Database.Interfaces;
using Database.Models;

namespace Database.Builders
{
    public class OrderBuilder : IDatabaseModelBuilder<Order>
    {
        Order order;
        public OrderBuilder()
        {
            order = new();
        }
        public OrderBuilder(Order order)
        {
            this.order = order;
        }
        public Order Build()
        {
            var order = this.order;
            this.order = new();
            return order;
        }
        public OrderBuilder SetId(int id)
        {
            this.order.Id = id;
            return this;
        }
        public OrderBuilder SetOrderItems(List<OrderItem> orderItems)
        {
            this.order.OrderItems = orderItems;
            return this;
        }
        public OrderBuilder SetNumber(string number)
        {
            this.order.Number = number;
            return this;
        }
        public OrderBuilder SetDate(DateTime date)
        {
            this.order.Date = date;
            return this;
        }
        public OrderBuilder SetProviderId(int providerId)
        {
            this.order.ProviderId = providerId;
            return this;
        }
    }
}
