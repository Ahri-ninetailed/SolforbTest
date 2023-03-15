using Database;
using MediatR;
using SolforbTest.Models;
using SolforbTest.Builders;
namespace SolforbTest.Features
{
    public class GetOrdersRequest : IRequest<IEnumerable<SolforbTest.Models.Order>>
    {
        public class GetOrdersRequestHandler : IRequestHandler<GetOrdersRequest, IEnumerable<SolforbTest.Models.Order>>
        {
            private readonly SolforbDbContext db;
            public GetOrdersRequestHandler(SolforbDbContext db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<Order>> Handle(GetOrdersRequest request, CancellationToken cancellationToken)
            {
                IEnumerable<Database.Models.Order> ordersFromDb = await db.GetOrdersAsync();
                List<SolforbTest.Models.Order> solforbOrders = new List<SolforbTest.Models.Order>();
                foreach (var order in ordersFromDb)
                {
                    List<SolforbTest.Models.OrderItem> solforbOrderItems = new List<SolforbTest.Models.OrderItem>();
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
