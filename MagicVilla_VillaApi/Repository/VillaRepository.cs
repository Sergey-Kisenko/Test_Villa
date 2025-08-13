using MagicVilla_VillaApi.Data;
using MagicVilla_VillaApi.Model;
using MagicVilla_VillaApi.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaApi.Repository
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly DbSet<Villa> _dbSet;
        public VillaRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Villa>();
        }

        public async Task<Villa> Update(Villa entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

      
    }
}
