using Microsoft.AspNetCore.Mvc;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetOrders()
        {
            var orders = new List<object>
            {
                new { OrderId = 101, Product = "Laptop", Quantity = 1 },
                new { OrderId = 102, Product = "Mouse", Quantity = 2 }
            };
            return Ok(orders);
        }
    }
}
