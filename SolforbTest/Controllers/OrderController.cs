using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SolforbTest.Controllers
{
    public class OrderController : Controller
    {
        private readonly SolforbDbContext solforbDbContext;
        public OrderController(SolforbDbContext solforbDbContext)
        {
            this.solforbDbContext = solforbDbContext;
        }
        [Route("/Order/{id}")]
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var order = await solforbDbContext.GetOrderByIdAsync(id);

            return View("Index", order);
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
    }
}
