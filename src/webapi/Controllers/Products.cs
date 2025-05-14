using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.DTO;
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
        public async Task<IEnumerable<Product>> FindAll(int currentPage, int ITEMS_PER_PAGE)
        {
            var products = await _productsService.findAll(currentPage, ITEMS_PER_PAGE);
            return products;
        }

        [HttpGet("findByCategory/{category}/{currentPage:int}/{ITEMS_PER_PAGE:int}")]
        public async Task<IEnumerable<Product>> FindByCategory(string category, int currentPage, int ITEMS_PER_PAGE)
        {
            var products = await _productsService.FindByCategory(category, currentPage, ITEMS_PER_PAGE);
            return products;
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
            var productDTO = await _productsService.GetProductPage(slug);
            return productDTO;
        }
    }
}

/*

export async function getProductPageData(slug : string) {
    const categories = await getCategories();
    const product = await productsService.getProductBySlug(slug);
    const category = await categoriesService.getById(product.categoryId);
    const products = await productsService.getSimilar(category.id, product.id);

    return { 
        productDTO: fromProductDomain(product, categories),
        productsDTO: fromProductsDomain(products, categories),
        categoryDTO: fromCategoryDomain(category)
    }
}
*/