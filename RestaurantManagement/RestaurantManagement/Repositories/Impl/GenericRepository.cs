using RestaurantManagement.DAOs;
using System.Linq.Expressions;

namespace RestaurantManagement.Repositories.Impl
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IGenericDAO<T> _dao;

        public GenericRepository(IGenericDAO<T> dao)
        {
            _dao = dao;
        }

        public async Task<T> AddAsync(T entity)
        {
            return await _dao.AddAsync(entity);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            return await _dao.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _dao.DeleteAsync(id);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dao.GetByIdAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dao.GetAllAsync();
        }

        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dao.FindByConditionAsync(predicate);
        }
    }
}
