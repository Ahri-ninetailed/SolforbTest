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
        public IActionResult Index(Order order)
        {
            return View(order);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}