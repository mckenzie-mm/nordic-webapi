using System.Net;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using webapi.DTO;
using webapi.DTO_mappings;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [Route("[controller]")]
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

        [HttpGet("{slug}")]
        public async Task<Product> Get(string slug)
        {
            var product = await _productsService.GetProduct(slug);
            return product;
        }

        [HttpGet("findAll/{currentPage:int}/{ITEMS_PER_PAGE:int}")]
        public async Task<IEnumerable<ProductDTO>> FindAll(int currentPage, int ITEMS_PER_PAGE)
        {
            var products = (List<Product>)await _productsService.findAll(currentPage, ITEMS_PER_PAGE);

            return Mapping.toProductsDTO(products);
        }

        [HttpGet("findByCategory/{category}/{currentPage:int}/{ITEMS_PER_PAGE:int}")]
        public async Task<IEnumerable<ProductDTO>> FindByCategory(string category, int currentPage, int ITEMS_PER_PAGE)
        {
            var products = (List<Product>)await _productsService.FindByCategory(category, currentPage, ITEMS_PER_PAGE);
            return Mapping.toProductsDTO(products);
        }

        [HttpGet("getSimilar/{category}/{id:int}")]
        public async Task<IEnumerable<Product>> GetSimilar(string category, int id)
        {
            var products = await _productsService.GetSimilar(category, id);
            return products;
        }

        [HttpGet("getProductPage/{slug}")]
        public async Task<ProductPageDTO> GetProductPage(string slug)
        {
            var ProductPageDTO = await _productsService.GetProductPage(slug);
            return ProductPageDTO;
        }

        [HttpGet("getCount")]
        public async Task<int> GetCount()
        {

            var count = await _productsService.GetCount();
            return count;
        }

        [HttpPost("form")]
        public IActionResult Form([FromForm]CreateProductRequest request)
        {

            var id = request.id;
            
            var product = request.ToDomain();

            var newPrice = Convert.ToInt32(request.price * 100);

            Console.WriteLine("item: ");
            Console.WriteLine(product.id);
            Console.WriteLine(request.price);
            Console.WriteLine(newPrice);
 

            // invoking the use case
            // _productsService.Create(product);

            // mapping to external representation
            return Ok(product);
        }
    }
}

public record CreateProductRequest(
    int id,
    string name,
    string slug,
    string category,
    string description,
    string smallImage,
    float price,
    int availability
)
{
    public Product ToDomain()
    {
        return new Product
        {
            id = id,
            name = name,
            slug = slug,
            category = category,
            smallImage = smallImage,
            mediumImage = smallImage,
            largeImage = smallImage,
            description = description,
            price =  Convert.ToInt32(price * 100),
            availability = availability
        };
    }
}

// public record ItemResponse(int Id, string Name, string Category)
// {
//     public static ItemResponse FromDomain(Item item)
//     {
//         return new ItemResponse(item.Id, item.Name, item.Category);
//     }
// }
