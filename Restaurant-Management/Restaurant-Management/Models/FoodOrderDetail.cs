using System;
using System.Collections.Generic;

namespace Restaurant_Management.Models;

public partial class FoodOrderDetail
{
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }

    public int FoodId { get; set; }

    public int PurchaseQuantity { get; set; }

    public bool? IsFeedback { get; set; }

    public virtual Food Food { get; set; } = null!;

    public virtual FoodOrder Order { get; set; } = null!;
}
