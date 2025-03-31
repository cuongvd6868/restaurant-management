using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.DTOs;
using RestaurantManagement.Models;
using RestaurantManagement.Repositories;
using RestaurantManagement.Services;
using System.Security.Claims;

[ApiController]
[Route("api/users")]
public class UsersController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;


    public UsersController(IUserRepository userRepository, IUserService userService)
    {
        _userRepository = userRepository;
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateUser([FromBody] UserCreationRequest request)
    {
        if (request == null)
        {
            return BadRequest("Dữ liệu không hợp lệ!");
        }

        var result = await _userService.CreateUser(request);
        if (result == null)
        {
            return BadRequest("Email đã tồn tại!");
        }

        return Ok(result);
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetUsers()
    {
        return Ok(await _userService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUsers(int id)
    {
        return Ok(await _userService.GetById(id));
    }

    [HttpGet("profile")] 
    public async Task<IActionResult> GetUserProfileView()
    {
        var currentUserId = GetUserId();

        if (!currentUserId.HasValue)
        {
            return RedirectToAction("Login", "Account");
        }

        var user = await _userService.GetById(currentUserId.Value);

        return View("UserProfile", user);
    }


    [HttpPut("ban/{id}")]
    public async Task<IActionResult> BanUser(int id)
    {
        return Ok(await _userService.BanById(id));
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update(UserEdit cus)
    {

        var user = _userRepository.GetById(cus.Id).Result;

        user.PhoneNumber = cus.PhoneNumber;
        user.Address = cus.Address;
        user.FirstName = cus.FirstName;
        user.LastName = cus.LastName;
        return Ok(await _userRepository.Update(user));
    }

    private int? GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return userIdClaim != null ? int.Parse(userIdClaim) : (int?)null;
    }
}
