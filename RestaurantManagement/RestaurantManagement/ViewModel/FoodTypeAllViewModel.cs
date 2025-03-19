using RestaurantManagement.DTOs;

namespace RestaurantManagement.ViewModel
{
    public class FoodTypeAllViewModel
    {
        public PagedListAPI<FoodViewModel> BreakfastFoods { get; set; }
        public PagedListAPI<FoodViewModel> LunchFoods { get; set; }
        public PagedListAPI<FoodViewModel> DinnerFoods { get; set; }
        public PagedListAPI<FoodViewModel> DessertFoods { get; set; }
    }
}
