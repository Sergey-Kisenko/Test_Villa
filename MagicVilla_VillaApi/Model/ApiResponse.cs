using System.Net;

namespace MagicVilla_VillaApi.Model
{
    public class ApiResponse
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public bool isSuccess { get; set; } = true;
        public List<string> ErrorMessege { get; set; }
        public object Result { get; set; }
    }
}
