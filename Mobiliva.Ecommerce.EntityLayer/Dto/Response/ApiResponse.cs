using System.Net.NetworkInformation;

namespace Mobiliva.Ecommerce.Data.Dto.Response
{
    public class ApiResponse<T>
    {
        public ApiStatus Status { get; set; }
        public string ResultMessage { get; set; }
        public int ErrorCode { get; set; }
        public T Data { get; set; }
    }
}
