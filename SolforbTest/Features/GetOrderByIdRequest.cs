using Database;
using MediatR;
using SolforbTest.Builders;
using SolforbTest.Models;

namespace SolforbTest.Features
{
    public class GetOrderByIdRequest : IRequest<Models.Order>
    {
        public int Id { get; set; }
        public class GetOrderByIdRequestHandler : IRequestHandler<GetOrderByIdRequest, Models.Order> 
        {
            private readonly SolforbDbContext db;
            public GetOrderByIdRequestHandler(SolforbDbContext db)
            {
                this.db = db;
            }

            public async Task<Order> Handle(GetOrderByIdRequest request, CancellationToken cancellationToken)
            {
                Database.Models.Order order = await db.GetOrderByIdAsync(request.Id);
                List<SolforbTest.Models.OrderItem> solforbOrderItems = new List<OrderItem>();
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
                return new OrderBuilder()
                    .SetId(order.Id)
                    .SetNumber(order.Number)
                    .SetProviderId(order.ProviderId)
                    .SetOrderItems(solforbOrderItems)
                    .SetDate(order.Date)
                    .Build();
            }
        }
    }
}
