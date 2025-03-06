using Microsoft.AspNetCore.Mvc;

namespace ProductServices.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = new List<object>
            {
                new { Id = 1, Name = "Laptop", Price = 50000 },
                new { Id = 2, Name = "Mouse", Price = 800 }
            };
            return Ok(products);
        }
    }
}
