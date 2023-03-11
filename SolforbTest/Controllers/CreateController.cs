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
        public async Task<IActionResult> Order(Order order, int providerId, string number)
        {
            
            if (!isOrderValidate(order))
            {
                fillOrderViewOfValidationErrors(order);
                return View("Index", order);
            }
            await solforbDbContext.CreateOrderAsync(order);
            if (order.OrderItems is null)
                order.OrderItems = new List<OrderItem>() { new OrderItem() };
            return View("ElementsOfOrder", await solforbDbContext.GetOrderByProviderAndNumberAsync(providerId, number));
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
    }
}