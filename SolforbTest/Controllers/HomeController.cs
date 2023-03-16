using Database;
using Database.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolforbTest.Features;
using SolforbTest.Features.Requests;
using SolforbTest.Models;
using System.Diagnostics;

namespace SolforbTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly SolforbDbContext solforbDbContext;
        private readonly IMediator mediator;

        public HomeController(SolforbDbContext solforbDbContext, IMediator mediator)
        {
            this.solforbDbContext = solforbDbContext;
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Orders = await mediator.Send(new GetOrdersRequest());
            return View(new GetFilteredOrdersRequest());
        }
        [Route("FilterOrders")]
        public async Task<IActionResult> Filter(GetFilteredOrdersRequest request)
        {
            ViewBag.Orders = await mediator.Send(request);
            return View("Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}