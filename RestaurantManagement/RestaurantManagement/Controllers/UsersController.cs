﻿using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.DTOs;
using RestaurantManagement.Services;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase  
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public UsersController(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
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
}
