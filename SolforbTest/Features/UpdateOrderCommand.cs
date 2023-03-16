using Database;
using Database.Updaters;
using MediatR;
using SolforbTest.Exceptions;

namespace SolforbTest.Features
{
    public class UpdateOrderCommand : IRequest
    {
        public Models.Order Order { get; set; }
        public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
        {
            private readonly SolforbDbContext db;
            public UpdateOrderCommandHandler(SolforbDbContext db)
            {
                this.db = db;
            }

            public async Task Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
            {
                if (command.Order.Number is null)
                    throw new RequiredOrderItemUnitException();
                //проверим, не существует ли такого заказа
                Database.Models.Order anotherOrder = await db.GetOrderByProviderAndNumberAsync(command.Order.ProviderId, command.Order.Number);
                if (anotherOrder is not null && anotherOrder.Id != command.Order.Id)
                    throw new ExistingOrderException();

                //обновим заказ
                await db.UpdateOrderAsync(new Database.Builders.OrderBuilder()
                    .SetId(command.Order.Id)
                    .SetNumber(command.Order.Number)
                    .SetProviderId(command.Order.ProviderId)
                    .SetDate(command.Order.Date)
                    .Build());
            }
        }
    }
}
