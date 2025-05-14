using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Services;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Seed(SeedService seedService) : ControllerBase
    {
        private readonly SeedService _seedService = seedService;
 
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await _seedService.Seed();
            return  Ok();
        }
    }
}
