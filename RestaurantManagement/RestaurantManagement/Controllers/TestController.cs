using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Repositories;

namespace RestaurantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IFoodRepository _foodRepository;

        public TestController(IFoodRepository foodRepository) 
        {
            _foodRepository = foodRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        { 
            return Ok(await _foodRepository.GetAllAsync());
        }
    }
}
