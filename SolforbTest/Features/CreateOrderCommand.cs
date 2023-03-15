using Database;
using MediatR;
using SolforbTest.Models;
using Database.Builders;
namespace SolforbTest.Features
{
    public class CreateOrderCommand : IRequest<Models.Order>
    {
        public Models.Order Order { get; set; }
        public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Models.Order> 
        {
            private readonly SolforbDbContext db;
            public CreateOrderCommandHandler(SolforbDbContext db)
            {
                this.db = db;
            }

            public async Task<Order> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
            {
                await db.CreateOrderAsync(new OrderBuilder()
                    .SetNumber(command.Order.Number)
                    .SetProviderId(command.Order.ProviderId)
                    .SetDate(command.Order.Date)
                    .Build());
                return command.Order;
            }
        }
    }
}
