using Database;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
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
            ViewBag.Providers = await solforbDbContext.GetProvidersAsync();
            return View(new Order() { OrderItems = new List<OrderItem>() { new OrderItem() } });
        }
        [HttpPost]
        public async Task<IActionResult> Index(Order order)
        {
            ViewBag.Providers = await solforbDbContext.GetProvidersAsync();
            if (!isValidate(order))
            {
                Console.WriteLine("TESTLSJDJTF:S:D");
                fillTheViewDataOfValidationErrors(order);
            }
            return View(order);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        

        private void fillTheViewDataOfValidationErrors(Order order)
        {
            string errorMsg = "Это поле обязательно к заполнению.";
            if (order.Number is null)
                ViewBag.NumberLabel = errorMsg;
            for (int i = 0; i < order.OrderItems.Count(); i++)
            {
                if (order.OrderItems[i].Name is null)
                    ViewData[$"NameLabel{i}"] = errorMsg;
                if (order.OrderItems[i].Unit is null)
                    ViewData[$"UnitLabel{i}"] = errorMsg;
                if (order.OrderItems[i].Quantity == 0)
                    ViewData[$"QuantityLabel{i}"] = errorMsg;
            }
        }
        private bool isValidate(Order order)
        {
            if (order.Number is null)
                return false;
            foreach (var orderItem in order.OrderItems)
                if (orderItem.Name is null || orderItem.Unit is null || orderItem.Quantity == 0)
                    return false;
            return true;
        }
    }
}