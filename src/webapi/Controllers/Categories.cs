using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Categories(CategoriesService categoriesService) : ControllerBase
    {
        private readonly CategoriesService _categoriesService = categoriesService;
        [HttpGet]
        public async Task<IEnumerable<Category>> Get()
        {
            var categories = await _categoriesService.Get();
            return categories;
        }

        // [HttpGet]
        // public IActionResult Seed()
        // {
        //     return  Ok();
        // }
    }
}
