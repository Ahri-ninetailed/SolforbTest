using Database;
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
        public async Task<IActionResult> Filter(FiltersModel filters)
        {
            
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}