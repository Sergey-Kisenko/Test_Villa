using MagicVillaWeb.Model.DTO;

namespace MagicVillaWeb.Services.Interfaces
{
    public interface IVillaNumberService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(VillaNumberDTOCreate dto);
        Task<T> UpdateAsync<T>(VillaNumberDTOUpdate dto);
        Task<T> DeleteAsync<T>(int id);

    }
}
