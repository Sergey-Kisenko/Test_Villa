using MagicVillaWeb.Model.DTO;

namespace MagicVillaWeb.Services.Interfaces
{
    public interface IVillaService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(VillaDTOCreate dto);
        Task<T> UpdateAsync<T>(VillaDTOUpdate dto);
        Task<T> DeleteAsync<T>(int id);

    }
}
