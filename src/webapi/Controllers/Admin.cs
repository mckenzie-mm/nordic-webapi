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
        public IActionResult PostProduct([FromForm] FormRequest request)
        {
            var product = request.toDomain();
            // invoking the use case
            // _productsService.Create(product);

            // mapping to external representation
            //return Ok(ProductDTO.fromDomain(product));

            // _context.TodoItems.Add(todoItem);
            // await _context.SaveChangesAsync();

            //    return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(GetProduct), new { id = 1000 }, ProductDTO.fromDomain(product));
        }

        [HttpPut("form/{id}")]
        public async Task<IActionResult> PutProduct(long id, [FromForm] FormRequest request)
        {
            // if (id != request.Id)
            // {
            //     return BadRequest();
            // }

            // _context.Entry(todoItem).State = EntityState.Modified;

            // try
            // {
            //     await _context.SaveChangesAsync();
            // }
            // catch (DbUpdateConcurrencyException)
            // {
            //     if (!TodoItemExists(id))
            //     {
            //         return NotFound();
            //     }
            //     else
            //     {
            //         throw;
            //     }
            // }

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
