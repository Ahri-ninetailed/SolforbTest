using Database;
using MediatR;

namespace SolforbTest.Features.Commands
{
    public class DeleteOrderByIdCommand : IRequest
    {
        public int OrderId { get; set; }
        public class DeleteOrderByIdCommandHandler : IRequestHandler<DeleteOrderByIdCommand>
        {
            private readonly SolforbDbContext db;
            public DeleteOrderByIdCommandHandler(SolforbDbContext db)
            {
                this.db = db;
            }

            public async Task Handle(DeleteOrderByIdCommand command, CancellationToken cancellationToken)
            {
                await db.DeleteOrderByIdAsync(command.OrderId);
            }
        }
    }
}
