using System;
using System.Collections.Generic;

namespace Restaurant_Management.Models;

public partial class FoodOrder
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public decimal TotalPrice { get; set; }

    public string? ShippingAddress { get; set; }

    public DateTime StartDate { get; set; }

    public string Status { get; set; } = null!;

    public int? PaymentMethodId { get; set; }

    public virtual ICollection<FoodOrderDetail> FoodOrderDetails { get; set; } = new List<FoodOrderDetail>();

    public virtual PaymentMethod? PaymentMethod { get; set; }

    public virtual User User { get; set; } = null!;
}
