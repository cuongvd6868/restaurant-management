using System;
using System.Collections.Generic;

namespace Restaurant_Management.Models;

public partial class Food
{
    public int FoodId { get; set; }

    public string FoodName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal ListPrice { get; set; }

    public decimal Price { get; set; }

    public int? FoodCategoryId { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual FoodCategory? FoodCategory { get; set; }

    public virtual ICollection<FoodFavorite> FoodFavorites { get; set; } = new List<FoodFavorite>();

    public virtual ICollection<FoodFeedback> FoodFeedbacks { get; set; } = new List<FoodFeedback>();

    public virtual ICollection<FoodImage> FoodImages { get; set; } = new List<FoodImage>();

    public virtual ICollection<FoodOrderDetail> FoodOrderDetails { get; set; } = new List<FoodOrderDetail>();
}
