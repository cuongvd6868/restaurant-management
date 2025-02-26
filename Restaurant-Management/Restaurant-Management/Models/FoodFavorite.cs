using System;
using System.Collections.Generic;

namespace Restaurant_Management.Models;

public partial class FoodFavorite
{
    public int FoodFavoriteId { get; set; }

    public int UserId { get; set; }

    public int FoodId { get; set; }

    public virtual Food Food { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
