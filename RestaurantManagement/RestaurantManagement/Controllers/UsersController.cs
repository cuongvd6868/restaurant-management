using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.DTOs;
using RestaurantManagement.Models;
using RestaurantManagement.Repositories;
using RestaurantManagement.Services;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase  
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
}
