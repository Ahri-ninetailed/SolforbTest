using Database;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolforbTest.Models;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace SolforbTest.Controllers
{
    public class CreateController : BaseController
    {
        private readonly SolforbDbContext solforbDbContext;

        public CreateController(SolforbDbContext solforbDbContext)
        {
            this.solforbDbContext = solforbDbContext;
        }
        public async Task<IActionResult> Order()
        {
            return View("Order");
        }
        [Route("/Order")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            //Валидация страницы создания заказа
            if (!isOrderValidate(order))
            {
                fillOrderViewOfValidationErrors(order);
                return View("Order", order);
            }
            //Создать заказ, если его еще нет
            var foundOrder = await solforbDbContext.GetOrderByProviderAndNumberAsync(order.ProviderId, order.Number);
            if (foundOrder is null)
            {
                await solforbDbContext.CreateOrderAsync(order);
            }
            else
            {
                ViewBag.NumberLabel = "Такой заказ от поставщика уже существует.";
                return View("Order", order);

            }
            return Redirect($"~/Order/{order.Id}");
        }
        [Route("Order/{orderId}/Item")]
        public async Task<IActionResult> Item(OrderItem orderItem)
        {
            return View("Item", orderItem);
        }
        [Route("/Order/{orderId}/Item",
            Name = "createitem")]
        [HttpPost]
        public async Task<IActionResult> CreateItem(OrderItem orderItem)
        {
            var order = await solforbDbContext.GetOrderByIdAsync(orderItem.OrderId);
            orderItem.OrderNumber= order.Number;
           
            if (!isOrderItemValidate(orderItem) || orderItem.Name == order.Number)
            {
                fillOrderItemViewOfValidationErrors(orderItem);
                return View("Item", orderItem);
            }
            else
            {
                await solforbDbContext.CreateOrderItemAsync(orderItem);
                order.OrderItems.Add(orderItem);
                await solforbDbContext.SaveChangesAsync();
            }
            return Redirect($"~/Order/{orderItem.OrderId}");
        }

        [Route("Order/DeleteItem/{itemId}")]
        [HttpDelete]
        public async Task<ObjectResult> DeleteItem(int itemId)
        {
            try
            {
                await solforbDbContext.DeleteOderItemByIdAsync(itemId);
                return Ok(null);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
    }
}