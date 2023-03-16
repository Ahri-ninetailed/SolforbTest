using Database;
using Database.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolforbTest.Exceptions;
using SolforbTest.Features.Commands;
using SolforbTest.Models;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace SolforbTest.Controllers
{
    public class CreateController : BaseController
    {
        private readonly IMediator mediator;
        public CreateController(SolforbDbContext solforbDbContext, IMediator mediator) : base(solforbDbContext) 
        { 
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        [Route("Order")]
        public async Task<IActionResult> Order()
        {
            return View("Order");
        }
        [Route("/Order")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
        {
            Models.Order order = null;
            try
            {
                order = await mediator.Send(command);
                return Redirect($"~/Order/{order.Id}");
            }
            catch(Exception ex)
            {
                fillOrderViewOfValidationErrors(ex);
                return View("Order", command);
            }
        }
        [Route("Order/{orderId}/Item")]
        public async Task<IActionResult> Item(int orderId)
        {
            return View("Item", new CreateOrderItemCommand 
            { 
                OrderItem = new Models.OrderItem 
                { 
                    OrderId = orderId 
                } 
            });
        }
        [Route("/Order/{orderId}/Item",
            Name = "createitem")]
        [HttpPost]
        public async Task<IActionResult> CreateItem(CreateOrderItemCommand command)
        {
            Models.OrderItem orderItem = null;
            try
            {
                orderItem = await mediator.Send(command);
                return Redirect($"~/Order/{orderItem.OrderId}");
            }
            catch (ValidationExceptions exceptions)
            {
                fillOrderItemViewOfValidationErrors(exceptions);
                return View("Item", command);
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
    }
}