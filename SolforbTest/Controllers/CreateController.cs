using Database;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolforbTest.Models;
using System.Diagnostics;

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
            var orderItems = order.OrderItems;
            if (orderItems is null)
                orderItems = new List<OrderItem>() { };
            order.OrderItems = orderItems;
            return View("ElementsOfOrder", order);
        }
        [Route("Order/{orderId}/Item")]
        [HttpPost]
        public async Task<IActionResult> Item(int orderId, OrderItem orderItem)
        {
            var order = await solforbDbContext.GetOrderByIdAsync(orderId);
            ViewBag.OrderId = orderId;
            if (orderItem.Name is null && orderItem.Unit is null && orderItem.Quantity == 0)
            {
                return View("Item");
            }
           
            if (!isOrderItemValidate(orderItem))
            {
                fillOrderItemViewOfValidationErrors(orderItem);

                return View("Item");
            }
            else
            {
                orderItem.OrderId = orderId;
                await solforbDbContext.CreateOrderItemAsync(orderItem);
                order.OrderItems.Add(orderItem);
                await solforbDbContext.SaveChangesAsync();
            }
            return Redirect($"~/Order/{orderId}");
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