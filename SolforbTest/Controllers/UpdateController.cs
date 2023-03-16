using Database;
using Database.Models;
using Database.Updaters;
using Microsoft.AspNetCore.Mvc;

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
            return View("Order", order);
        }
        [Route("Update/Order/{orderId}")]
        [HttpPost]
        public async Task<IActionResult> Order(int orderId, Order order)
        {
            //валидация заказа
            if (!isOrderValidate(order))
            {
                //метод заполняет представление ошибками валидации
                fillOrderViewOfValidationErrors(order);
                return View("Order", order);
            }
            //проверим, не существует ли такого заказа
            Order anotherOrder = await solforbDbContext.GetOrderByProviderAndNumberAsync(order.ProviderId, order.Number);
            if (anotherOrder is not null && anotherOrder.Id != orderId)
            {
                string errorMessage = "Заказ от такого поставщика уже существует";
                ViewBag.NumberLabel = errorMessage;
                ViewBag.ProviderLabel = errorMessage;
                return View("Order", order);
            }
            //обновим заказ
            Order foundOrder = await solforbDbContext.GetOrderByIdAsync(orderId);
            foundOrder.UpdateOrder(order);
            await solforbDbContext.UpdateOrderAsync(foundOrder);
            return Redirect($"~/Order/{orderId}");
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
