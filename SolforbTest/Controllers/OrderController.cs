using Database;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SolforbTest.Features;

namespace SolforbTest.Controllers
{
    public class OrderController : Controller
    {
        private readonly SolforbDbContext solforbDbContext;
        private readonly IMediator mediator;

        public OrderController(SolforbDbContext solforbDbContext, IMediator mediator)
        {
            this.solforbDbContext = solforbDbContext;
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        [Route("/Order/{id}")]
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var order = await solforbDbContext.GetOrderByIdAsync(id);

            return View("Index", order);
        }
        [Route("Order/DeleteOrder/{orderId}")]
        [HttpDelete]
        public async Task DeleteOrder(DeleteOrderByIdCommand command)
        {
            await mediator.Send(command);
        }
        [Route("Order/DeleteItem/{itemId}")]
        [HttpDelete]
        public async Task DeleteItem(DeleteOrderItemByIdCommand command)
        {
            await mediator.Send(command);
        }
    }
}
