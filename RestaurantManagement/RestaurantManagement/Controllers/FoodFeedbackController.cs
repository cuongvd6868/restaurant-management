using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.DTOs;
using RestaurantManagement.Models;
using System.Security.Claims;

namespace RestaurantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodFeedbackController : ControllerBase
    {
        private readonly FoodDbContext _context;

        public FoodFeedbackController(FoodDbContext context)
        {
            _context = context;
        }
        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> AddFeedback([FromBody] FoodFeedbackDTO request)
        {
            if (request == null)
            {
                return BadRequest("Invalid feedback data.");
            }
            var userId = GetUserId();
            if (userId == null) return Unauthorized("User is not logged in");

            FoodFeedback feedback = new FoodFeedback { UserID = (int)userId, FoodID = request.FoodID, RatingPoint = request.RatingPoint, Comment = request.Comment };
            _context.FoodFeedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            return Ok("Feedback added successfully.");
        }

        [HttpGet("list/{foodId}")]
        public IActionResult GetFeedbackByFood(int foodId)
        {
            var feedbackList = _context.FoodFeedbacks.Include(X=>X.Customer).Where(f => f.FoodID == foodId).ToList();
            if (!feedbackList.Any())
            {
                return NotFound("No feedback found for this food.");
            }
            return Ok(feedbackList);
        }
        [Authorize]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateFeedback(int id, [FromBody] FoodFeedbackDTO updatedFeedback)
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized("User is not logged in");
            var feedback = await _context.FoodFeedbacks.FindAsync(id);
            if (feedback == null)
            {
                return NotFound("Feedback not found.");
            }
            if (feedback.UserID != (int)userId) return Unauthorized("You do not have permission to update");

            feedback.RatingPoint = updatedFeedback.RatingPoint;
            feedback.Comment = updatedFeedback.Comment;

            _context.FoodFeedbacks.Update(feedback);
            await _context.SaveChangesAsync();
            return Ok("Feedback updated successfully.");
        }
        private int? GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userIdClaim != null ? int.Parse(userIdClaim) : (int?)null;
        }
    }
}
