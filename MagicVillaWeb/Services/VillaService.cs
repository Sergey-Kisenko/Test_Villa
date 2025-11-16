using MagicVillaUtility;
using MagicVillaWeb.Model.DTO;
using MagicVillaWeb.Models;
using MagicVillaWeb.Services.Interfaces;

namespace MagicVillaWeb.Services
{
    public class VillaService : BaseService, IVillaService
    {
        private readonly IHttpClientFactory _clientFactory;
        public string villaUrl;
        public VillaService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }


        public Task<T> CreateAsync<T>(VillaDTOCreate dto)
        {
            return SendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.POST,
                Data = dto,
                URL = villaUrl+ "/api/VillaApi"
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.DELETE,
                URL = villaUrl + "/api/VillaApi/" + id
            });
        }


        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.GET,
                URL = villaUrl + "/api/VillaApi"
            });
        }


        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.GET,
                URL = villaUrl + "/api/VillaApi/" +id
            });
        }

        public Task<T> UpdateAsync<T>(VillaDTOUpdate dto)
        {
            return SendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.PUT,
                Data = dto,
                URL = villaUrl + "/api/VillaApi/"+ dto.Id
            });
        }
    }
}
