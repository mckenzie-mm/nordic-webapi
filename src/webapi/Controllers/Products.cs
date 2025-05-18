using System.Net;
using Microsoft.AspNetCore.Mvc;
using webapi.Domain;
using webapi.DTO;
using webapi.Services;

namespace webapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Products(ProductsService productsService, CategoriesService categoriesService) : ControllerBase
    {
        private readonly ProductsService _productsService = productsService;
        private readonly CategoriesService _categoriesService = categoriesService;
        
        [HttpGet("findAll/{currentPage:int}/{ITEMS_PER_PAGE:int}")]
        public async Task<ProductsResponse> FindAll(int currentPage, int ITEMS_PER_PAGE)
        {
            var products = (List<Product>)await _productsService.findAll(currentPage, ITEMS_PER_PAGE);

            return ProductsResponse.fromDomain(products);
        }

        [HttpGet("findByCategory/{category}/{currentPage:int}/{ITEMS_PER_PAGE:int}")]
        public async Task<ProductsResponse> FindByCategory(string category, int currentPage, int ITEMS_PER_PAGE)
        {
            var products = (List<Product>)await _productsService.FindByCategory(category, currentPage, ITEMS_PER_PAGE);
            return ProductsResponse.fromDomain(products);
        }

        [HttpGet("getProductPage/{slug}")]
        public async Task<ProductPage> GetProductPage(string slug)
        {
            var productPage = await _productsService.GetProductPage(slug);
            return productPage;
        }

        [HttpGet("getCount")]
        public async Task<int> GetCount()
        {
            var count = await _productsService.GetCount();
            return count;
        }

        [HttpPost("form")]
        public IActionResult Create([FromForm] FormRequest request)
        {
            var id = request.id;
            var product = request.toDomain();
            // invoking the use case
            // _productsService.Create(product);

            // mapping to external representation
            return Ok(product);
        }
        
        [HttpGet("form/{slug}")]
        public async Task<IActionResult> GetForm(string slug)
        {
            // invoking the use case
            var product = await _productsService.GetProduct(slug);
            var categories = (List<Category>) await _categoriesService.Get();
            // mapping to external representation
            return Ok(FormResponse.fromDomain(product, categories));
        }
    }
}


