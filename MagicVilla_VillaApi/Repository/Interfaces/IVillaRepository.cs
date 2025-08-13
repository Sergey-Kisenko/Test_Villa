using MagicVilla_VillaApi.Model;

namespace MagicVilla_VillaApi.Repository.Interfaces
{
    public interface IVillaRepository:IRepository<Villa>
    {
        public Task<Villa> Update(Villa entity);
    }
}
