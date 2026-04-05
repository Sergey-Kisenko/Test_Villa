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


        public Task<T> CreateAsync<T>(VillaDTOCreate dto, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.POST,
                Data = dto,
                URL = villaUrl+ "/api/VillaApi",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.DELETE,
                URL = villaUrl + "/api/VillaApi/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.GET,
                URL = villaUrl + "/api/VillaApi",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.GET,
                URL = villaUrl + "/api/VillaApi/" +id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(VillaDTOUpdate dto, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.PUT,
                Data = dto,
                URL = villaUrl + "/api/VillaApi/"+ dto.Id,
                Token = token
            });
        }
    }
}
