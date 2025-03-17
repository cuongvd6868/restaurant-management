﻿using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Models;
using System.Linq.Expressions;

namespace RestaurantManagement.DAOs.Impl
{
    public class GenericDAO<T> : IGenericDAO<T> where T : class
    {
        private readonly FoodDbContext _context;

        public GenericDAO(FoodDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            var entityEntry = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entityEntry.Entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null) return false;
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }
    }
}
