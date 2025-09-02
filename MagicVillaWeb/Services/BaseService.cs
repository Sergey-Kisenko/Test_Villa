using MagicVillaUtility;
using MagicVillaWeb.Model;
using MagicVillaWeb.Models;
using MagicVillaWeb.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace MagicVillaWeb.Services
{
    public class BaseService : IBaseServices
    {
        public ApiResponse responseModel { get; set; }
        public IHttpClientFactory clientFactory { get; set; }
        public BaseService(IHttpClientFactory clientFactory)
        {
            responseModel = new ApiResponse();
            this.clientFactory = clientFactory;
        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = clientFactory.CreateClient("MagicAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept","application/json");
                message.RequestUri = new Uri(apiRequest.URL);
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8,"application/json");
                }
                switch (apiRequest.apiType)
                {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                HttpResponseMessage apiResponse = null;

                apiResponse =  await client.SendAsync(message);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent); ;

                return APIResponse;

            }
            catch (Exception ex)
            {
                var dto = new ApiResponse
                {
                    ErrorMessege = new List<string> { Convert.ToString(ex.Message) },
                    isSuccess = false

                };
                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;
            }
        }
    }
}
