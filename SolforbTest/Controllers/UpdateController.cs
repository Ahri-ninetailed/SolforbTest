using Database;
using Database.Updaters;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using SolforbTest.Features.Requests;
using System;
using SolforbTest.Exceptions;
using SolforbTest.Models;
using SolforbTest.Features.Commands;

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
            Order order = await mediator.Send(new GetOrderByIdRequest() { Id = orderId });
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
            OrderItem orderItem = await mediator.Send(new GetOrderItemByIdRequest() { Id = orderItemId });
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
