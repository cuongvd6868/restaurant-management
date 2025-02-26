using System;
using System.Collections.Generic;

namespace Restaurant_Management.Models;

public partial class FoodCategory
{
    public int FoodCategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Food> Foods { get; set; } = new List<Food>();
}
