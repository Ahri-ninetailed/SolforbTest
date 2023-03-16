using Database;
using MediatR;
using System.Security.AccessControl;

namespace SolforbTest.Features
{
    public class GetOrderIdByOrderItemIdRequest : IRequest<int> 
    {
        public int OrderItemId { get; set; }
        public class GetOrderIdByOrderItemIdRequestHandler : IRequestHandler<GetOrderIdByOrderItemIdRequest, int>
        {
            private readonly SolforbDbContext db;
            public GetOrderIdByOrderItemIdRequestHandler(SolforbDbContext db)
            {
                this.db = db;
            }

            public async Task<int> Handle(GetOrderIdByOrderItemIdRequest request, CancellationToken cancellationToken)
            {
                return await db.GetOrderIdByOrderItemId(request.OrderItemId);
            }
        }
    }
}
