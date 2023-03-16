using Database;
using Database.Models;
using MediatR;
using SolforbTest.Models;
using SolforbTest.Builders;

namespace SolforbTest.Features.Requests
{
    public class GetFilteredOrdersRequest : IRequest<IEnumerable<Models.Order>>
    {
        public FiltersModel FiltersModel { get; set; }
        public class GetFilteredOrdersRequestHandler : IRequestHandler<GetFilteredOrdersRequest, IEnumerable<Models.Order>>
        {
            private readonly SolforbDbContext db;
            public GetFilteredOrdersRequestHandler(SolforbDbContext db)
            {
                this.db = db;
            }

            public async Task<IEnumerable<Models.Order>> Handle(GetFilteredOrdersRequest request, CancellationToken cancellationToken)
            {
                IEnumerable<Database.Models.Order> ordersFromDb = await db.GetOrdersAsync();
                var filteredOrdersFromDb = ordersFromDb
                    .OrdersByDates(request.FiltersModel.FirstDate, request.FiltersModel.SecondDate)
                    .OrdersByProvidersId(request.FiltersModel.ProvidersId)
                    .OrdersByNumbers(request.FiltersModel.OrdersNumbers)
                    .OrdersByItemsNames(request.FiltersModel.ItemsNames)
                    .OrdersByItemsQuantities(request.FiltersModel.ItemsQuantities)
                    .OrdersByItemsUnits(request.FiltersModel.ItemsUnits)
                    .OrdersByProvidersNames(request.FiltersModel.ProvidersNames, await db.GetProvidersAsync());
                List<Models.Order> solforbOrders = new List<Models.Order>();
                foreach (var order in filteredOrdersFromDb)
                {
                    List<Models.OrderItem> solforbOrderItems = new List<Models.OrderItem>();
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
