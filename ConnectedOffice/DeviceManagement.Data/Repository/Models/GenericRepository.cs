using DeviceManagement.Data.Repository.Interfaces;
using DeviceManagement.Database.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManagement.Data.Repository.Models
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ConnectedOfficeContext _context;

        public GenericRepository(ConnectedOfficeContext context)
        {
            _context = context;
        }

        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression)
        {
            return await Task.FromResult(_context.Set<T>().Where(expression));
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Task.FromResult(_context.Set<T>().ToList());
        }

        public async Task<T> GetById(Guid? id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task Remove(T entity)
        {
            await Task.Run(() => _context.Set<T>().Remove(entity));
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRange(IEnumerable<T> entities)
        {
            await Task.Run(() => _context.Set<T>().RemoveRange(entities));
            await _context.SaveChangesAsync();
        }
    }
}
