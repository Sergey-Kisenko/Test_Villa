using static MagicVillaUtility.SD;
namespace MagicVillaWeb.Models
{
    public class ApiRequest
    {
        public ApiType apiType { get; set; } = ApiType.GET;
        public object Data { get; set; }
        public string URL { get; set; }
    }
}
