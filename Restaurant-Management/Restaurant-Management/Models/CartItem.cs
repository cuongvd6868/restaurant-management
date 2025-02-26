using System;
using System.Collections.Generic;

namespace Restaurant_Management.Models;

public partial class CartItem
{
    public int CartItemId { get; set; }

    public int UserId { get; set; }

    public int FoodId { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public virtual Food Food { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
