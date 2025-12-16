using MagicVilla_VillaApi.Model;
using MagicVilla_VillaApi.Model.DTO;

namespace MagicVilla_VillaApi.Repository.Interfaces
{
    public interface IVillaRepository:IRepository<Villa>
    {
        public Task<Villa> Update(Villa entity);
    }
}
