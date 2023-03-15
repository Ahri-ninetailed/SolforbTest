using Database;
using MediatR;
using System.Runtime.CompilerServices;

namespace SolforbTest.Features
{
    public class DeleteOrderItemByIdCommand : IRequest
    {
        public int ItemId { get; set; }
        public class DeleteOrderItemByIdCommandHandler : IRequestHandler<DeleteOrderItemByIdCommand>
        {
            private readonly SolforbDbContext db;
            public DeleteOrderItemByIdCommandHandler(SolforbDbContext db)
            {
                this.db = db;
            }
            public async Task Handle(DeleteOrderItemByIdCommand command, CancellationToken cancellationToken)
            {
                await db.DeleteOderItemByIdAsync(command.ItemId);
            }
        }
    }
}
