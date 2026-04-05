using MagicVillaWeb.Model.DTO;

namespace MagicVillaWeb.Services.Interfaces
{
    public interface IVillaNumberService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(VillaNumberDTOCreate dto, string token);
        Task<T> UpdateAsync<T>(VillaNumberDTOUpdate dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);

    }
}
