using Microsoft.OpenApi.Models;

namespace MobilivaEcommerceAPI.RestExtention
{
    public static class CustomSwaggerExtension
    {
        public static void AddCustomSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mobiliva ECommerce Api", Version = "v1.0" });
            });
        }
    }
}
