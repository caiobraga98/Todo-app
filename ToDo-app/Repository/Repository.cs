
using Microsoft.EntityFrameworkCore;
using ToDo_app.Data;

namespace ToDo_app.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        protected readonly DataDbContext _context;

        public Repository(DataDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(T entity)
        {
            var result = _context.Add(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var result = _context.Remove(_context.Find<T>(id)!);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
             var result = _context.Find<T>(id);
                return await Task.FromResult(result);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
