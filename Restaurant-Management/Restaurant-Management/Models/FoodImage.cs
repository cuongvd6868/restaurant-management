using System;
using System.Collections.Generic;

namespace Restaurant_Management.Models;

public partial class FoodImage
{
    public int ImageId { get; set; }

    public int FoodId { get; set; }

    public string? ImageName { get; set; }

    public byte[]? ImageData { get; set; }

    public string? ImageLink { get; set; }

    public virtual Food Food { get; set; } = null!;
}
