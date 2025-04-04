﻿using System.Text.Json.Serialization;

namespace RestaurantManagement.Models
{
    public class FoodImage
    {
        public int FoodID { get; set; }
        public int ImageID { get; set; }
        public string ImageName { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageLink { get; set; }
        [JsonIgnore]
        public virtual Food Food { get; set; }
    }
}