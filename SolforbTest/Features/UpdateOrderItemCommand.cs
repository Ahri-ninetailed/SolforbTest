﻿using Database;
using Database.Updaters;
using MediatR;
using SolforbTest.Exceptions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SolforbTest.Features
{
    public class UpdateOrderItemCommand : IRequest, IOrderItemRequest
    {
        public SolforbTest.Models.OrderItem OrderItem { get; set; }
        public class UpdateOrderItemCommandHandler : OrderItemCommandHandlerBase, IRequestHandler<UpdateOrderItemCommand>
        {
            private readonly SolforbDbContext db;
            public UpdateOrderItemCommandHandler(SolforbDbContext db)
            {
                this.db = db;
            }

            public async Task Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
            {
                var order = await db.GetOrderByIdAsync(request.OrderItem.OrderId);
                request.OrderItem.OrderNumber = order.Number;
                var listOfExceptions = fillValidationExceptionsList(request);
                if (listOfExceptions.Count() > 0)
                    throw new ValidationExceptions(listOfExceptions);

                var orderItem = await db.GetOrderItemByIdAsync(request.OrderItem.Id);
                await db.UpdateOrderItemAsync(new Database.Builders.OrderItemBuilder(orderItem)
                    .SetName(request.OrderItem.Name)
                    .SetQuantity(request.OrderItem.Quantity)
                    .SetUnit(request.OrderItem.Unit)
                    .Build());
            }
        }
    }
}
