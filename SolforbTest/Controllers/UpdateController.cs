using Database;
using Database.Models;
using Database.Updaters;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using SolforbTest.Features;
using System;
using SolforbTest.Exceptions;

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
            SolforbTest.Models.OrderItem orderItem = await mediator.Send(new GetOrderItemByIdRequest() { Id = orderItemId });
            return View("Item", new UpdateOrderItemCommand() { OrderItem = orderItem });
        }
        [Route("Update/OrderItem/{Id}")]
        [HttpPost]
        public async Task<IActionResult> Item(int id, UpdateOrderItemCommand command)
        {
            command.OrderItem.Id = id;
            command.OrderItem.OrderId = await mediator.Send(new GetOrderIdByOrderItemIdRequest() { OrderItemId = id });
            try
            {
                await mediator.Send(command);
                return Redirect($"~/Order/{command.OrderItem.OrderId}");
            }
            catch (ValidationExceptions exceptions)
            {
                fillOrderItemViewOfValidationErrors(exceptions);
                return View("Item", command);
            }
        }
    }
}
