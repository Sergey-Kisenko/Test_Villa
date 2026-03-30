using MagicVillaWeb.Model.DTO;
using MagicVillaWeb.Models.DTO;

namespace MagicVillaWeb.Services.Interfaces
{
    public interface IAuthService
    {
        Task<T> LoginyAsync<T>(LoginRequestDTO objToCreate);
        Task<T> Register<T>(RegisterationRequestDTO objToCreate);
    }
}
