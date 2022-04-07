using iotlink_webapi.DataModels;

namespace iotlink_webapi
{
    public class Response<T>
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
