using MagicVilla_VillaApi.Model.DTO;

namespace MagicVilla_VillaApi.Service.Interfaces
{
    public interface IVillaNumberService
    {
        public Task Update(VillaNumberDTOUpdate numberDTOUpdate);

    }
}
