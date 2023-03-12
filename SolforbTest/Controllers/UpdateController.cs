using Database;
using Database.Models;
using Database.Updaters;
using Microsoft.AspNetCore.Mvc;

namespace SolforbTest.Controllers
{
    public class UpdateController : BaseController
    {
        public UpdateController(SolforbDbContext solforbDbContext) : base(solforbDbContext) { }
        [Route("Update/Order/{orderId}")]
        [HttpGet]
        public async Task<IActionResult> Order(int orderId)
        {
            Order order = await solforbDbContext.GetOrderByIdAsync(orderId);
            return View("Order", order);
        }
        [Route("Update/Order/{orderId}")]
        [HttpPost]
        public async Task<IActionResult> Order(int orderId, Order order)
        {
            if (!isOrderValidate(order))
            {
                fillOrderViewOfValidationErrors(order);
                return View("Order", order);
            }
            Order anotherOrder = await solforbDbContext.GetOrderByProviderAndNumberAsync(order.ProviderId, order.Number);
            if (anotherOrder is not null)
            {
                string errorMessage = "Заказ от такого поставщика уже существует";
                ViewBag.NumberLabel = errorMessage;
                ViewBag.ProviderLabel = errorMessage;
                return View("Order", order);
            }
            Order foundOrder = await solforbDbContext.GetOrderByIdAsync(orderId);
            foundOrder.UpdateOrder(order);
            await solforbDbContext.UpdateOrderAsync(foundOrder);
            return Redirect($"~/Order/{orderId}");
        }
    }
}
