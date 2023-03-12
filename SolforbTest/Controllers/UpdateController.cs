using Database;
using Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace SolforbTest.Controllers
{
    public class UpdateController : BaseController
    {
        public UpdateController(SolforbDbContext solforbDbContext) : base(solforbDbContext) { }
        [Route("Update/Order/{orderId}")]
        [HttpGet]
        public async Task<IActionResult> Order(int orderId)
        {
            Order order = await solforbDbContext.GetOrderByIdAsync(orderId);
            return View("Order", order);
        }
    }
}
