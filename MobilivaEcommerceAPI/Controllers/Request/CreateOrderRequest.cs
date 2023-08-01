namespace MobilivaEcommerceAPI.Controllers.Request
{
    public class CreateOrderRequest
    {
        public String CustomerName { get; set; }
        public String CustomerEmail { get; set; }
        public  String CustomerGsm { get; set; }

        public  List<ProductDetail>  ProductDetails { get; set; }


    }
}
