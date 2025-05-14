using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Products(ProductsService productsService) : ControllerBase
    {
        private readonly ProductsService _productsService = productsService;
        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            var products = await _productsService.Get();
            return products;
        }
    }
}
