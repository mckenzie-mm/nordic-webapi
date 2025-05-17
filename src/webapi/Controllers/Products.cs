using System.Net;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using webapi.Controllers;
using webapi.DTO;
using webapi.DTO_mappings;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Products(ProductsService productsService, CategoriesService categoriesService) : ControllerBase
    {
        private readonly ProductsService _productsService = productsService;
        private readonly CategoriesService _categoriesService = categoriesService;
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
        public IActionResult Create([FromForm] CreateProductRequest request)
        {
            var id = request.id;
            var product = request.ToDomain();
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
            return Ok(ProductResponse.FromDomain(product, categories));
        }
    }
}


