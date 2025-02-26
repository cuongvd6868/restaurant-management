using System;
using System.Collections.Generic;

namespace Restaurant_Management.Models;

public partial class PaymentMethod
{
    public int PaymentMethodId { get; set; }

    public string PaymentMethodName { get; set; } = null!;

    public decimal? PaymentCost { get; set; }

    public virtual ICollection<FoodOrder> FoodOrders { get; set; } = new List<FoodOrder>();
}
