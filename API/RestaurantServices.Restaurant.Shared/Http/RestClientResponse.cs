using System.Net;

namespace RestaurantServices.Restaurant.Shared.Http
{
    public class RestClientResponse<T>
    {
        public int StatusCode { get; set; }
        public HttpStatusCode StatusName { get; set; }
        public string Message { get; set; }
        public T Response { get; set; }
    }

    public class RestClientResponse
    {
        public int StatusCode { get; set; }
        public HttpStatusCode StatusName { get; set; }
        public string Message { get; set; }
        public string Response { get; set; }
    }
}
