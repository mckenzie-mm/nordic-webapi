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

            Console.WriteLine("request");
            Console.WriteLine(product.id);
            Console.WriteLine(product.name);
            Console.WriteLine(product.description);

            // invoking the use case
            await _productsService.Create(product);

            Console.WriteLine("res");

            var createdProduct = await _productsService.GetProduct(product.slug);

            return CreatedAtAction(nameof(GetProduct), new { id = 1000 }, ProductDTO.fromDomain(createdProduct));
        }

        [HttpPut("form/{id}")]
        public async Task<IActionResult> PutProduct(int id, [FromForm] FormRequest request)
        {

            var product = request.toDomain();
            Console.WriteLine("request");

            // Console.WriteLine(ProductDTO.fromDomain(product));

            Console.WriteLine(product.name);
            Console.WriteLine(product.price);
            Console.WriteLine(product.smallImage);
            Console.WriteLine(product.mediumImage);
            Console.WriteLine(product.largeImage);
            Console.WriteLine(product.slug);
            Console.WriteLine(product.description) ;
            Console.WriteLine(product.availability) ;
            Console.WriteLine(product.category) ;

            var res = await _productsService.UpdateAsync(id, product);
          
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(long id)
        {
            // var todoItem = await _context.TodoItems.FindAsync(id);

            // if (todoItem == null)
            // {
            //     return NotFound();
            // }

            // return todoItem;
            return null;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            // var todoItem = await _context.TodoItems.FindAsync(id);
            // if (todoItem == null)
            // {
            //     return NotFound();
            // }

            // _context.TodoItems.Remove(todoItem);
            // await _context.SaveChangesAsync();

            return NoContent();
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
