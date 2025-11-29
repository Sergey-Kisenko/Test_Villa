using MagicVillaUtility;
using MagicVillaWeb.Model.DTO;
using MagicVillaWeb.Models;
using MagicVillaWeb.Services.Interfaces;

namespace MagicVillaWeb.Services
{
    public class VillaNumberService : BaseService, IVillaNumberService
    {
        private readonly IHttpClientFactory _clientFactory;
        public string villaUrl;
        public VillaNumberService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }


        public Task<T> CreateAsync<T>(VillaNumberDTOCreate dto)
        {
            return SendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.POST,
                Data = dto,
                URL = villaUrl+ "/api/VillaNumber"
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.DELETE,
                URL = villaUrl + "/api/VillaNumber/" + id
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.GET,
                URL = villaUrl + "/api/VillaNumber"
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.GET,
                URL = villaUrl + "/api/VillaNumber/" + id
            });
        }

        public Task<T> UpdateAsync<T>(VillaNumberDTOUpdate dto)
        {
            return SendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.PUT,
                Data = dto,
                URL = villaUrl + "/api/VillaNumber/" + dto.VillaNo
            });
        }
    }
}
