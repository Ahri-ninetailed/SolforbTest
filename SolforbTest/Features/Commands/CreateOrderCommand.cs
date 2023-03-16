using Database;
using MediatR;
using SolforbTest.Models;
using Database.Builders;
using SolforbTest.Exceptions;
using Database.Models;
using SolforbTest.Extensions;

namespace SolforbTest.Features.Commands
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

            public async Task<Models.Order> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
            {
                if (command.Order.Number is null)
                    throw new RequiredFieldException();

                var foundOrder = await db.GetOrderByProviderAndNumberAsync(command.Order.ProviderId, command.Order.Number);
                if (foundOrder is not null)
                    throw new ExistingOrderException();
                var createdOrder = await db.CreateOrderAsync(new OrderBuilder()
                    .SetNumber(command.Order.Number)
                    .SetProviderId(command.Order.ProviderId)
                    .SetDate(command.Order.Date)
                    .Build());
                command.Order.Id = createdOrder.Id;
                return command.Order;
            }
        }
    }
}
