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
        public Task<T> CreateAsync<T>(VillaNumberDTOCreate dto, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.POST,
                Data = dto,
                URL = villaUrl+ "/api/VillaNumber",
                Token = token
            });
        }
        public Task<T> DeleteAsync<T>(int VillaNo, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.DELETE,
                URL = villaUrl + "/api/VillaNumber/" + VillaNo,
                Token = token
            });
        }
        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.GET,
                URL = villaUrl + "/api/VillaNumber",
                Token = token
            });
        }
        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.GET,
                URL = villaUrl + "/api/VillaNumber/" + id,
                Token = token
            });
        }
        public Task<T> UpdateAsync<T>(VillaNumberDTOUpdate dto, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.PUT,
                Data = dto,
                URL = villaUrl + "/api/VillaNumber/" + dto.VillaNo,
                Token = token
            });
        }
    }
}
