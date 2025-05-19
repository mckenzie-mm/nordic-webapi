using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Domain;
using webapi.DTO;
using webapi.Services;

namespace webapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Admin(ProductsService productsService, CategoriesService categoriesService) : ControllerBase
    {
        private readonly ProductsService _productsService = productsService;
        private readonly CategoriesService _categoriesService = categoriesService;

        [HttpPost("form")]
        public IActionResult Create([FromForm] FormRequest request)
        {
            var id = request.id;
            var product = request.toDomain();
            // invoking the use case
            // _productsService.Create(product);

            // mapping to external representation
            return Ok(ProductDTO.fromDomain(product));
        }

        [HttpGet("form/{slug}")]
        public async Task<IActionResult> GetForm(string slug)
        {
            // invoking the use case
            var product = await _productsService.GetProduct(slug);
            var categories = (List<Category>)await _categoriesService.Get();
            // mapping to external representation
            return Ok(FormResponse.fromDomain(product, categories));
        }
        
        [HttpGet("count")]
        public async Task<int> GetCount()
        {
            var count = await _productsService.GetCount();
            return count;
        }
    }
}
