using MagicVillaUtility;
using MagicVillaWeb.Model.DTO;
using MagicVillaWeb.Models;
using MagicVillaWeb.Services.Interfaces;

namespace MagicVillaWeb.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IHttpClientFactory _clientFactory;
        public string villaUrl;
        public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> LoginyAsync<T>(LoginRequestDTO dto)
        {
            return SendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.POST,
                Data = dto,
                URL = villaUrl + "/api/UsersAuth/login"
            });
        }

        public Task<T> Register<T>(RegisterationRequestDTO dto)
        {
            return SendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.POST,
                Data = dto,
                URL = villaUrl + "/api/UsersAuth/register"
            });
        }
    }
}
