using MagicVilla_VillaApi.Data;
using MagicVilla_VillaApi.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVilla_VillaApi.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDBContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDBContext context)
        {
            _context = context;
            _context.VillaNumbers.Include(u => u.Villa).ToList();
            _dbSet = context.Set<T>();
        }
        public async Task<T?> Get(Expression<Func<T, bool>> filter = null, bool tracked = false, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if(includeProperties!= null)
            {
                foreach (var incliderProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incliderProp);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var incliderProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incliderProp);
                }
            }

            return await query.ToListAsync();
        }
        public async Task<T> Create(T entity)
        {
            await _dbSet.AddAsync(entity);
            await Save();
            return entity;
        }

        public async Task<Task> Delete(T entity)
        {
            _dbSet.Remove(entity);
            await Save();
            return Task.CompletedTask;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
      
    }

}
