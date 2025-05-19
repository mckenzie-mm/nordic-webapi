using System.Net;
using Microsoft.AspNetCore.Mvc;
using webapi.Domain;
using webapi.DTO;
using webapi.Services;

namespace webapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Products(ProductsService productsService) : ControllerBase
    {
        private readonly ProductsService _productsService = productsService;
        
        [HttpGet("list/{currentPage:int}/{ITEMS_PER_PAGE:int}")]
        public async Task<ProductDTOList> Get(int currentPage, int ITEMS_PER_PAGE)
        {
            var products = (List<Product>)await _productsService.findAll(currentPage, ITEMS_PER_PAGE);

            return ProductDTOList.fromDomain(products);
        }

        [HttpGet("list/{category}/{currentPage:int}/{ITEMS_PER_PAGE:int}")]
        public async Task<ProductDTOList> GetByCategory(string category, int currentPage, int ITEMS_PER_PAGE)
        {
            var products = (List<Product>)await _productsService.FindByCategory(category, currentPage, ITEMS_PER_PAGE);
            return ProductDTOList.fromDomain(products);
        }

        [HttpGet("page/{slug}")]
        public async Task<Page> GetProductPage(string slug)
        {
            Product product = await _productsService.GetProduct(slug);
            List<Product> similar = (List<Product>) await _productsService.GetSimilar(product.category, product.id);
            var page = Page.fromDomain(product, similar);
            return page;
        }

    }
}


