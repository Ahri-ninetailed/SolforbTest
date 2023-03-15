using Database;
using Database.Builders;
using MediatR;
using SolforbTest.Exceptions;
namespace SolforbTest.Features
{
    public class CreateOrderItemCommand : IRequest<Models.OrderItem>
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
        public class CreateOrderItemCommandHandler : IRequestHandler<CreateOrderItemCommand, Models.OrderItem>
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
            private IEnumerable<Exception> fillValidationExceptionsList(CreateOrderItemCommand command)
            {
                List<Exception> listOfValidationExceptions = new List<Exception>();
                if (command.OrderItem.Unit is null)
                    listOfValidationExceptions.Add(new RequiredOrderItemUnitException());
                if (command.OrderItem.Name is null)
                    listOfValidationExceptions.Add(new RequiredOrderItemNameException());
                else if (command.OrderItem.Name == command.OrderItem.OrderNumber)
                    listOfValidationExceptions.Add(new EqualOrderNumberAndOrderItemNameException());
                if (command.OrderItem.Quantity <= 0)
                    listOfValidationExceptions.Add(new RequiredOrderItemQuantityException());
                return listOfValidationExceptions;
            }
        }
    }
}
