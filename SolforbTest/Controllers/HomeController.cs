using Database;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolforbTest.Models;
using System.Diagnostics;

namespace SolforbTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly SolforbDbContext solforbDbContext;

        public HomeController(SolforbDbContext solforbDbContext)
        {
            this.solforbDbContext = solforbDbContext;
        }
        public async Task<IActionResult> Index()
        {
            var orders = await solforbDbContext.GetOrdersAsync();
            ViewBag.Orders = orders;
            return View();
        }
        [Route("FilterOrders")]
        public async Task<IActionResult> Filter(FiltersModel filters)
        {
            var orders = await solforbDbContext.GetOrdersAsync();
            ViewBag.Orders = orders
                .OrdersByDates(filters.FirstDate, filters.SecondDate)
                .OrdersByProvidersId(filters.ProvidersId)
                .OrdersByNumbers(filters.OrdersNumbers)
                .OrdersByItemsNames(filters.ItemsNames)
                .OrdersByItemsQuantities(filters.ItemsQuantities)
                .OrdersByItemsUnits(filters.ItemsUnits)
                .OrdersByProvidersNames(filters.ProvidersNames, await solforbDbContext.GetProvidersAsync());
            return View("Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}