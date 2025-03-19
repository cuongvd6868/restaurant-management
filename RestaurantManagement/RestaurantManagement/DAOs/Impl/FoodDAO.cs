using Microsoft.EntityFrameworkCore;
using RestaurantManagement.DTOs;
using RestaurantManagement.Models;
using System.Linq.Expressions;

namespace RestaurantManagement.DAOs.Impl
{
    public class FoodDAO : GenericDAO<Food>, IFoodDAO
    {
        private readonly FoodDbContext _context;

        public FoodDAO(FoodDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<FoodCategory>> GetFoodCategories()
        {
            return await _context.FoodCategories.ToListAsync();
        }

        public async Task<PagedList> GetFooodsAsync(int? cateId, string? search, int pageNumber, int pageSize)
        {
            var query = _context.Foods.Include(f => f.FoodCategory).AsQueryable();
            if (cateId.HasValue)
            {
                query = query.Where(f => f.FoodCategoryID == cateId);
            }
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(f => f.FoodName.Contains(search));
            }
            var count = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedList { Items = items, TotalCount = count, PageNumber = pageNumber, PageSize = pageSize };

        }
        public async Task<Food> CreateFoodAsync(Food food)
        {
            _context.Foods.Add(food);
            await _context.SaveChangesAsync();
            return food;
        }

        public async Task<Food> UpdateFoodAsync(Food food)
        {
            _context.Foods.Update(food);
            await _context.SaveChangesAsync();
            return food;
        }

        public async Task<bool> DeleteFoodAsync(int foodId)
        {
            var food = await _context.Foods
                .Include(f => f.FoodImages)
                .Include(f => f.FoodFavorites)
                .Include(f => f.FoodOrderDetails)
                .Include(f => f.FoodFeedbacks)
                .Include(f => f.CartItems)
                .FirstOrDefaultAsync(f => f.FoodID == foodId);

            if (food == null)
            {
                return false;
            }

            _context.FoodImages.RemoveRange(food.FoodImages);
            _context.FoodFavorites.RemoveRange(food.FoodFavorites);
            _context.FoodOrderDetails.RemoveRange(food.FoodOrderDetails);
            _context.FoodFeedbacks.RemoveRange(food.FoodFeedbacks);
            _context.CartItems.RemoveRange(food.CartItems);

            _context.Foods.Remove(food);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
