using Database;
using Database.Builders;
using MediatR;
using SolforbTest.Exceptions;
namespace SolforbTest.Features
{
    public class CreateOrderItemCommand : IRequest<Models.OrderItem>, IOrderItemRequest
    {
        private int orderId;
        public int OrderId { 
            get 
            { 
                return orderId;
            } 
            set 
            { 
                orderId = value;
                OrderItem.OrderId = value;
            } 
        }
        public Models.OrderItem OrderItem { get; set; } = new Models.OrderItem();
        public class CreateOrderItemCommandHandler : OrderItemCommandHandlerBase, IRequestHandler<CreateOrderItemCommand, Models.OrderItem>
        {
            private readonly SolforbDbContext db;
            public CreateOrderItemCommandHandler(SolforbDbContext db)
            {
                this.db = db;
            }
            public async Task<Models.OrderItem> Handle(CreateOrderItemCommand command, CancellationToken cancellationToken)
            {
                var order = await db.GetOrderByIdAsync(command.OrderItem.OrderId);
                command.OrderItem.OrderNumber = order.Number;
                var listOfExceptions = fillValidationExceptionsList(command);
                if (listOfExceptions.Count() > 0)
                    throw new ValidationExceptions(listOfExceptions);
                var orderItem = await db.CreateOrderItemAsync(new OrderItemBuilder()
                    .SetUnit(command.OrderItem.Unit)
                    .SetName(command.OrderItem.Name)
                    .SetOrderId(command.OrderItem.OrderId)
                    .SetOrderNumber(command.OrderItem.OrderNumber)
                    .SetQuantity(command.OrderItem.Quantity)
                    .Build());
                order.OrderItems.Add(orderItem);
                await db.SaveChangesAsync();
                command.OrderItem.Id = orderItem.Id;
                return command.OrderItem;
            }
        }
    }
}
