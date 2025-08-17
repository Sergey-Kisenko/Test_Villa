using MagicVilla_VillaApi.Model;

namespace MagicVilla_VillaApi.Repository.Interfaces
{
    public interface IVillaNumberRepository:IRepository<VillaNumber>
    {
        public Task<VillaNumber> Update(VillaNumber entity);
    }
}
