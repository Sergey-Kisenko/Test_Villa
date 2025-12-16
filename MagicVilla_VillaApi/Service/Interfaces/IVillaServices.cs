using MagicVilla_VillaApi.Model.DTO;

namespace MagicVilla_VillaApi.Service.Interfaces
{
    public interface IVillaServices
    {
        public Task Update(VillaDTOUpdate villaDTOUpdate);
    }
}
