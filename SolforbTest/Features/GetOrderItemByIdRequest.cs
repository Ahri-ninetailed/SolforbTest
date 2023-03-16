﻿using Database;
using MediatR;
using SolforbTest.Models;

namespace SolforbTest.Features
{
    public class GetOrderItemByIdRequest : IRequest<Models.OrderItem>
    {
        public int Id { get; set; }
        public class GetOrderItemByIdRequestHandler : IRequestHandler<GetOrderItemByIdRequest, Models.OrderItem>
        {
            private readonly SolforbDbContext db;
            public GetOrderItemByIdRequestHandler(SolforbDbContext db)
            {
                this.db = db;
            }

            public async Task<OrderItem> Handle(GetOrderItemByIdRequest request, CancellationToken cancellationToken)
            {
                Database.Models.OrderItem orderItemFromDb = await db.GetOrderItemByIdAsync(request.Id);
                return new Builders.OrderItemBuilder()
                    .SetId(orderItemFromDb.Id)
                    .SetName(orderItemFromDb.Name)
                    .SetUnit(orderItemFromDb.Unit)
                    .SetQuantity(orderItemFromDb.Quantity)
                    .SetOrderId(orderItemFromDb.OrderId)
                    .SetOrderNumber(orderItemFromDb.OrderNumber)
                    .Build();
            }
        }
    }
}
