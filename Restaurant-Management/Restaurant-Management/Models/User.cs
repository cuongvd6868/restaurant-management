using System;
using System.Collections.Generic;

namespace Restaurant_Management.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? ShippingAddress { get; set; }

    public string? Avatar { get; set; }

    public bool? IsActive { get; set; }

    public int? ActivId { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<FoodFavorite> FoodFavorites { get; set; } = new List<FoodFavorite>();

    public virtual ICollection<FoodFeedback> FoodFeedbacks { get; set; } = new List<FoodFeedback>();

    public virtual ICollection<FoodOrder> FoodOrders { get; set; } = new List<FoodOrder>();

    public virtual Role? Role { get; set; }
}
