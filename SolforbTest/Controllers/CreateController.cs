using Database;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolforbTest.Models;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace SolforbTest.Controllers
{
    public class CreateController : Controller
    {
        private readonly SolforbDbContext solforbDbContext;

        public CreateController(SolforbDbContext solforbDbContext)
        {
            this.solforbDbContext = solforbDbContext;
        }
        public async Task<IActionResult> Index()
        {
            return View(new Order());
        }
        [Route("/Order/{id}")]
        [HttpGet]
        public async Task<IActionResult> Order(int id)
        {
            var order = await solforbDbContext.GetOrderByIdAsync(id);

            return View("ElementsOfOrder", order);
        }
        [Route("/Order")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            //Валидация страницы создания заказа
            if (!isOrderValidate(order))
            {
                fillOrderViewOfValidationErrors(order);
                return View("Index", order);
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
                return View("Index", order);

            }
            var orderItems = order.OrderItems;
            if (orderItems is null)
                orderItems = new List<OrderItem>() { };
            order.OrderItems = orderItems;
            return View("ElementsOfOrder", order);
        }
        [Route("Order/{orderId}/Item")]
        [HttpPost]
        public async Task<IActionResult> Item(OrderItem orderItem)
        {
            var order = await solforbDbContext.GetOrderByIdAsync(orderItem.OrderId);
            ViewBag.OrderId = orderItem.OrderId;
            orderItem.OrderNumber= order.Number;
           
            if (!isOrderItemValidate(orderItem) || orderItem.Name == order.Number)
            {
                fillOrderItemViewOfValidationErrors(orderItem);
                return View("Item");
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
        [Route("Order/DeleteOrder/{orderId}")]
        [HttpDelete]
        public async Task<ObjectResult> DeleteOrder(int orderId)
        {
            try
            {
                await solforbDbContext.DeleteOrderByIdAsync(orderId);
                return Ok(null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Save(Order order, int providerId, string number)
        {
            /*var orderItems = order.OrderItems;
            order = await solforbDbContext.GetOrderByProviderAndNumberAsync(providerId, number);
            if (!isOrderItemsValidate(orderItems))
            {
                order.OrderItems = orderItems;
                fillOrderItemsViewOfValidationErrors(orderItems);
                return View("ElementsOfOrder", order);
            }
            foreach (var item in orderItems)
            {
                item.OrderId = order.Id;
                await solforbDbContext.CreateOrderItemAsync(item);
            }
            await solforbDbContext.UpdateOrderAsync(order);*/
            return View("Views/Home/Index.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        

        private void fillOrderViewOfValidationErrors(Order order)
        {
            string errorMsg = "Это поле обязательно к заполнению.";
            if (order.Number is null)
                ViewBag.NumberLabel = errorMsg;
        }
        private bool isOrderValidate(Order order)
        {
            return (order.Number is null ? false : true);

        }
        private void fillOrderItemViewOfValidationErrors(OrderItem item)
        {
            string errorMsg = "Это поле обязательно к заполнению.";
            if (item.Unit is null)
                ViewData[$"UnitError"] = errorMsg;
            if (item.Name is null)
                ViewData[$"NameError"] = errorMsg;
            if (item.Name == item.OrderNumber)
                ViewData[$"NameError"] = "Название позиции и номер заказа должны быть разными";
            if (item.Quantity <= 0)
                ViewData[$"QuantityError"] = errorMsg;
        }
        private bool isOrderItemValidate(OrderItem item)
        {
            if (item.Name is null || item.Unit is null || item.Quantity <= 0)
                 return false;
            return true;
        }
    }
}