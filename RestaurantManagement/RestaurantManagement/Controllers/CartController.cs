﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Models;
using RestaurantManagement.Repositories;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RestaurantManagement.Controllers
{
    [Authorize] 
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartItemRepository _cartItemRepository;

        public CartController(ICartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        [HttpGet]
        [Route("GetCart")]
        public async Task<IActionResult> GetCart()
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized("User is not logged in");

            var cartItems = await _cartItemRepository.GetListCartItemsByCurrentUser(userId.Value);
            return Ok(cartItems);
        }

        [HttpGet]
        [Route("AddToCart")]
        public async Task<IActionResult> AddToCart(int foodId, int quantity, decimal price)
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized("User is not logged in");
            var checkExist = (await _cartItemRepository.FindByConditionAsync( x=> x.FoodID ==foodId && x.UserID == userId)).FirstOrDefault();
            if (checkExist != null)
            {
                checkExist.Quantity += quantity;
                await _cartItemRepository.UpdateAsync(checkExist);
                return Ok(checkExist);
            }


            var cartItem = new CartItem
            {
                FoodID = foodId,
                Quantity = quantity,
                Price = price,
                UserID = userId.Value
            };

            await _cartItemRepository.AddAsync(cartItem);
            return Ok(cartItem);
        }

        [HttpGet]
        [Route("UpdateCartItem/{cartItemId}")]
        public async Task<IActionResult> UpdateCartItem(int cartItemId, int quantity)
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized("User is not logged in");

            var cartItem = await _cartItemRepository.GetByIdAsync(cartItemId);
            if (cartItem == null) return NotFound("Cart item not found");
            if (cartItem.UserID != userId) return Forbid(); // Kiểm tra quyền sở hữu

            cartItem.Quantity = quantity;
            await _cartItemRepository.UpdateAsync(cartItem);
            return Ok(cartItem);
        }

        [HttpGet]
        [Route("RemoveFromCart/{cartItemId}")]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized("User is not logged in");

            var cartItem = await _cartItemRepository.GetByIdAsync(cartItemId);
            if (cartItem == null) return NotFound("Cart item not found");
            if (cartItem.UserID != userId) return Forbid(); // Kiểm tra quyền sở hữu

            await _cartItemRepository.DeleteAsync(cartItemId);
            return Ok(new { message = "Item removed from cart" });
        }


        private int? GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userIdClaim != null ? int.Parse(userIdClaim) : (int?)null;
        }
    }
}
