using MagicVilla_VillaApi.Data;
using MagicVilla_VillaApi.Model;
using MagicVilla_VillaApi.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaApi.Repository
{
    public class VillaNumberRepository: Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly DbSet<VillaNumber> _dbSet;
        public VillaNumberRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<VillaNumber>();
        }

        public async Task<VillaNumber> Update(VillaNumber entity)
        {
            entity.DateTimeUpdate = DateTime.UtcNow;
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

    }
}
