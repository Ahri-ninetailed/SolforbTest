using Database;
using Database.Models;
using Database.Updaters;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using SolforbTest.Features;

namespace SolforbTest.Controllers
{
    public class UpdateController : BaseController
    {
        private readonly IMediator mediator;
        public UpdateController(SolforbDbContext solforbDbContext, IMediator mediator) : base(solforbDbContext) 
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        [Route("Update/Order/{orderId}")]
        [HttpGet]
        public async Task<IActionResult> Order(int orderId)
        {
            SolforbTest.Models.Order order = await mediator.Send(new GetOrderByIdRequest() { Id = orderId });
            return View("Order", new UpdateOrderCommand() { Order = order });
        }
        [Route("Update/Order/{orderId}")]
        [HttpPost]
        public async Task<IActionResult> Order(int orderId, UpdateOrderCommand command)
        {
            command.Order.Id = orderId;
            try
            {
                await mediator.Send(command);
                return Redirect($"~/Order/{orderId}");
            }
            catch (Exception ex)
            {
                fillOrderViewOfValidationErrors(ex);
                return View("Order", command);
            }
        }
        [Route("Update/OrderItem/{orderItemId}")]
        [HttpGet]
        public async Task<IActionResult> Item(int orderItemId)
        {
            OrderItem orderItem = await solforbDbContext.GetOrderItemByIdAsync(orderItemId);
            return View("Item", orderItem);
        }
        [Route("Update/OrderItem/{Id}")]
        [HttpPost]
        public async Task<IActionResult> Item(OrderItem orderItem)
        {
            if (!isOrderItemValidate(orderItem))
            {
                fillOrderItemViewOfValidationErrors(orderItem);
                return View("Item", orderItem);
            }
            orderItem.OrderId = await solforbDbContext.GetOrderIdByOrderItemId(orderItem.Id);
            Order order = await solforbDbContext.GetOrderByIdAsync(orderItem.OrderId);
            if (order.Number == orderItem.Name)
            {
                string errorMessage = "Название позиции не может совпадать с номером заказа";
                ViewBag.NameError = errorMessage;
                return View("Item", orderItem);
            }
            OrderItem foundOrderItem = await solforbDbContext.GetOrderItemByIdAsync(orderItem.Id);
            foundOrderItem.UpdateOrderItem(orderItem);
            await solforbDbContext.UpdateOrderItemAsync(foundOrderItem);
            return Redirect($"~/Order/{foundOrderItem.OrderId}");
        }
    }
}
