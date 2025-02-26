using System;
using System.Collections.Generic;

namespace Restaurant_Management.Models;

public partial class FoodFeedback
{
    public int FoodFeedbackId { get; set; }

    public int UserId { get; set; }

    public int FoodId { get; set; }

    public decimal? RatingPoint { get; set; }

    public string? Comment { get; set; }

    public virtual Food Food { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
