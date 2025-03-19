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

        public async override Task<Food> GetByIdAsync(int id)
        {
            return await _context.Foods.Include(f => f.FoodCategory)
                .Include(f => f.FoodImages)
                .FirstOrDefaultAsync(f => f.FoodID == id);
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
            if (!string.IsNullOrEmpty(search)) {
                query = query.Where(f=>f.FoodName.Contains(search));
            }
            var count = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedList { Items = items, TotalCount = count, PageNumber = pageNumber, PageSize = pageSize };

        }
    }
}
