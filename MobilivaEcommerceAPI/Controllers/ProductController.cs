using Microsoft.AspNetCore.Mvc;
using Mobiliva.Ecommerce;
using Mobiliva.Ecommerce.Data;
using Mobiliva.Ecommerce.Data.Domain;
using Mobiliva.Ecommerce.Data.Dto.Request;
using Mobiliva.Ecommerce.Data.Dto.Response;
using Mobiliva.Ecommerce.EntityLayer.Dto;
using Mobiliva.Ecommerce.Operation;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Pkcs;
using RabbitMQ.Client;
using System.Text;

namespace MobilivaEcommerceAPI.Controllers
{
    [Route("mobilivaapi/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService productService;
        private IOrderService orderService;
        private readonly IConnection _rabbitMqConnection;

        public ProductController (IProductService productService, IOrderService orderService, IConnection rabbitMqConnection)
        {
            this.productService = productService;
            this.orderService = orderService;
            _rabbitMqConnection = rabbitMqConnection;
        }

        [HttpGet()]
        public ApiResponse<List<ProductDto>> GetAll(string? category)
        {
            ApiResponse<List<ProductDto>> result = new ApiResponse<List<ProductDto>>();

            if(String.IsNullOrEmpty(category))
            {
                result = productService.GetProducts();
            }
            else
            {
                result=productService.GetProducts(category);
            }

            return result;
        }

        [HttpPost]
        public ApiResponse<string> CreateOrder([FromBody] CreateOrderRequest request)
        {
            ApiResponse<string> order = orderService.CreateOrder(request);

            // Asenkron mail gönderim isteğini RabbitMQ kuyruğuna ekleyin
            string message = "Sipariş Oluşturuldu.";

            SendMailQueueMessage(message);

            return order;
        }


        private void SendMailQueueMessage(string message)
        {
            // RabbitMQ kuyruğuna asenkron mail gönderim isteğini ekleyin
            using (var channel = _rabbitMqConnection.CreateModel())
            {
                channel.QueueDeclare(queue: "SendMailQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "SendMailQueue",
                                     basicProperties: null,
                                     body: body);
            }
        }

    }

}
