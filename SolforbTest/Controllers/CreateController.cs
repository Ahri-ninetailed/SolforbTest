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
        [HttpPost]
        public async Task<IActionResult> Order(Order order, int providerId, string number, int? countElements = 1)
        {
            
            if (!isOrderValidate(order))
            {
                fillOrderViewOfValidationErrors(order);
                return View("Index", order);
            }
            if (await solforbDbContext.GetOrderByProviderAndNumberAsync(providerId, number) is null)
                await solforbDbContext.CreateOrderAsync(order);

            if (order.OrderItems is null)
                order.OrderItems = new List<OrderItem>() { new OrderItem() };
            return View("ElementsOfOrder", await solforbDbContext.GetOrderByProviderAndNumberAsync(providerId, number));
        }
        [HttpPost]
        public async Task<IActionResult> Save(Order order, int providerId, string number)
        {
            var orderItems = order.OrderItems;
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
            await solforbDbContext.UpdateOrderAsync(order);
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
        private void fillOrderItemsViewOfValidationErrors(IEnumerable<OrderItem> items)
        {
            string errorMsg = "Это поле обязательно к заполнению.";
            var listOfItems = items.ToList();
            for (int i = 0; i < listOfItems.Count(); i++)
            {
                if (listOfItems[i].Unit is null)
                    ViewData[$"UnitError{i}"] = errorMsg;
                if (listOfItems[i].Name is null)
                    ViewData[$"NameError{i}"] = errorMsg;
                if (listOfItems[i].Quantity <= 0)
                    ViewData[$"QuantityError{i}"] = errorMsg;
            }
        }
        private bool isOrderItemsValidate(IEnumerable<OrderItem> items)
        {
            foreach (var item in items)
            {
                if (item.Name is null || item.Unit is null || item.Quantity <= 0)
                    return false;
            }
            return true;
        }
    }
}