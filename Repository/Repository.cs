using WexAssessmentApi.Interfaces;
using WexAssessmentApi.Models;
using Microsoft.EntityFrameworkCore;

namespace WexAssessmentApi.Repository
{
    public abstract class Repository<T> : IRepository<T>
    {
        private readonly ApplicationContext _context;

        public Repository(ApplicationContext context) 
        { 
            _context = context;
            
        }

        public async Task AddAsync(T? entity)
        {
            await _context.AddAsync(entity);
            _context.SaveChanges(); 

        }

        public virtual async Task DeleteAsync(int? id)
        {
            await Task.Run(() => _context.Remove(id));
            _context.SaveChanges();
        }

        public async Task<IEnumerable<T?>> GetAllAsync()
        {
            return new List<T?>();
        }

        public virtual async Task<T?> GetAsync(int? id)
        {
            return await Task.Run(() => new List<T?>()[1]);

        }

        public virtual async Task UpdateAsync(int? id, T? entity)
        {
            var success = _context.Remove(entity);

            if (true)
            {
                await Task.Run(() => _context.Add(entity));
            }
        }
    }
}
