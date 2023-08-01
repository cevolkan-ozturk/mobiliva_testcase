using Mobiliva.Ecommerce;
using Mobiliva.Ecommerce.Operation;
using RabbitMQ.Client;

namespace MobilivaEcommerceAPI.RestExtention
{
    public static class ServiceExtension
    {

        public static void AddServiceExtension(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();

            // RabbitMQ bağlantı nesnesini ekleyin
            services.AddSingleton<IConnection>(sp =>
            {
                var factory = new ConnectionFactory()
                {
                    HostName = "localhost", // RabbitMQ sunucu adresi
                    Port = 5672, // Varsayılan RabbitMQ portu
                    UserName = "guest", // RabbitMQ kullanıcı adı
                    Password = "guest" // RabbitMQ şifresi
                };
                return factory.CreateConnection();
            });
        }
    }
}
