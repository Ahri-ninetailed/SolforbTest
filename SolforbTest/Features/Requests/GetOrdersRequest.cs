using Database;
using MediatR;
using SolforbTest.Models;
using SolforbTest.Builders;

namespace SolforbTest.Features.Requests
{
    public class GetOrdersRequest : IRequest<IEnumerable<Order>>
    {
        public class GetOrdersRequestHandler : IRequestHandler<GetOrdersRequest, IEnumerable<Order>>
        {
            private readonly SolforbDbContext db;
            public GetOrdersRequestHandler(SolforbDbContext db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<Order>> Handle(GetOrdersRequest request, CancellationToken cancellationToken)
            {
                IEnumerable<Database.Models.Order> ordersFromDb = await db.GetOrdersAsync();
                List<Order> solforbOrders = new List<Order>();
                foreach (var order in ordersFromDb)
                {
                    List<OrderItem> solforbOrderItems = new List<OrderItem>();
                    foreach (var item in order.OrderItems)
                    {
                        solforbOrderItems.Add(new OrderItemBuilder()
                            .SetId(item.Id)
                            .SetOrderId(item.OrderId)
                            .SetOrderNumber(item.OrderNumber)
                            .SetUnit(item.Unit)
                            .SetQuantity(item.Quantity)
                            .SetName(item.Name)
                            .Build());
                    }
                    solforbOrders.Add(new OrderBuilder()
                        .SetId(order.Id)
                        .SetDate(order.Date)
                        .SetNumber(order.Number)
                        .SetOrderItems(solforbOrderItems)
                        .SetProviderId(order.ProviderId)
                        .Build());
                }
                return solforbOrders;
            }
        }
    }
}
