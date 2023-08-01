using Mobiliva.Ecommerce.Data.Domain;

namespace Mobiliva.Ecommerce.Data.Dto.Request
{
    public class CreateOrderRequest
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerGsm { get; set; }
        public List<OrderDetail> orderDetails { get; set; }

    }
}
