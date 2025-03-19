using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Models;
using RestaurantManagement.Repositories;

namespace RestaurantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodImageController : ControllerBase
    {
        private readonly IFoodImageRepository _foodImageRepository;

        public FoodImageController(IFoodImageRepository foodImageRepository)
        {
            _foodImageRepository = foodImageRepository;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFoodImage(int foodId, IFormFile file)
        {
            // Kiểm tra nếu tệp không có dữ liệu
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            // Đọc tệp tin và chuyển thành byte[]
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var imageData = memoryStream.ToArray();

                // Tạo đối tượng FoodImage và lưu vào database
                var foodImage = new FoodImage
                {
                    FoodID = foodId,
                    ImageName = file.FileName,
                    ImageData = imageData,
                    ImageLink = "" // Nếu cần, có thể lưu link đường dẫn nếu dùng lưu trữ ngoài
                };

                await _foodImageRepository.AddAsync(foodImage);

                return Ok(new { message = "Image uploaded successfully." });
            }
        }

        [HttpGet("get/{imageId}")]
        public async Task<IActionResult> GetFoodImage(int imageId)
        {
            var foodImage = await _foodImageRepository.GetByFoodIdAsync(imageId);

            if (foodImage == null)
            {
                return NotFound(); // Nếu không tìm thấy ảnh
            }

            // Trả về ảnh dưới dạng FileResult
            return File(foodImage.ImageData, "image/jpeg"); // Hoặc kiểu ảnh khác như PNG, GIF tùy theo loại ảnh
        }

    }
}
