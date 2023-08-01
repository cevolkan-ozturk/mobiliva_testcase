using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Mobiliva.Ecommerce.Data.Dto;
using MobilivaEcommerceAPI.RestExtention;

namespace MobilivaEcommerceAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddControllersWithViews(options =>
            options.CacheProfiles.Add(ResponseCasheType.Minute45, new CacheProfile
            {
                Duration = 45 * 60,
                NoStore = false,
                Location = ResponseCacheLocation.Any
            }));

            services.AddResponseCompression();
            services.AddMemoryCache();
            services.AddCustomSwaggerExtension();
            services.AddDbContextExtension(Configuration);
            services.AddMapperExtension();
            services.AddRepositoryExtension();
            services.AddServiceExtension();

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DefaultModelsExpandDepth(-1);
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mobiliva API");
                c.DocumentTitle = "Mobiliva API";
            });

            //DI
            //app.AddExceptionHandler();
            //app.AddDIExtension();
            //app.UseHangfireDashboard();

            //app.UseMiddleware<HeartBeatMiddleware>();
            //app.UseMiddleware<ErrorHandlerMiddleware>();
            //Action<RequestProfilerModel> requestResponseHandler = requestProfilerModel =>
            //{
            //    Log.Information("-------------Request-Begin------------");
            //    Log.Information(requestProfilerModel.Request);
            //    Log.Information(Environment.NewLine);
            //    Log.Information(requestProfilerModel.Response);
            //    Log.Information("-------------Request-End------------");
            //};
            //app.UseMiddleware<RequestLoggingMiddleware>(requestResponseHandler);

            app.UseHttpsRedirection();

            // add auth 
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
