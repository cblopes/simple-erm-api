using SimpleERP.MVC.Extensions;
using SimpleERP.MVC.Services;

namespace SimpleERP.MVC.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAuthService, AuthService>();

            services.AddHttpClient<IClientService, ClientService>();

            services.AddHttpClient<IProductService, ProductService>();

            services.AddHttpClient<IOrderService, OrderService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUser, AspNetUser>();
        }
    }
}
