using System.Threading.Tasks;
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
        public async Task<IActionResult> PostProduct([FromForm] FormRequest request)
        {
            var product = request.toDomain();

            // invoking the use case
            await _productsService.Create(product);

            var createdProduct = await _productsService.GetProduct(product.id);

            return CreatedAtAction(nameof(GetProductBySlug), new { product.slug }, ProductDTO.fromDomain(createdProduct));
        }

        [HttpPut("form/{id:int}")]
        public async Task<IActionResult> PutProduct(int id, [FromForm] FormRequest request)
        {

            var product = request.toDomain();
         
            await _productsService.UpdateAsync(id, product);

            return NoContent();
        }

        [HttpGet("{slug}")]
        public async Task<ActionResult<ProductDTO>> GetProductBySlug(string slug)
        {
            var product = await _productsService.GetProductBySlug(slug);
            if (product == null)
            {
                return NotFound();
            }
            return ProductDTO.fromDomain(product);
        }

       
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productsService.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            await _productsService.DeleteProduct(product.id);

            return NoContent();
        }


        [HttpGet("form/{slug}")]
        public async Task<IActionResult> GetForm(string slug)
        {
            // invoking the use case
            var product = await _productsService.GetProductBySlug(slug);
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
