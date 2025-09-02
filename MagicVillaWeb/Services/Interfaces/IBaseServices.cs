using MagicVillaWeb.Model;
using MagicVillaWeb.Models;

namespace MagicVillaWeb.Services.Interfaces
{
    public interface IBaseServices
    {
        ApiResponse responseModel { get; set; }
         Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
