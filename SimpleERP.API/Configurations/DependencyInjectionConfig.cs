using FluentValidation;
using SimpleERP.API.Data.Repositories;
using SimpleERP.API.Interfaces;
using SimpleERP.API.Models.Validators;
using SimpleERP.API.Models;
using SimpleERP.API.Services;

namespace SimpleERP.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IClientServices, ClientServices>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductServices, ProductServices>();

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderServices, OrderServices>();

            services.AddScoped<IOrderItemRepository, OrderItemRepository>();

            services.AddTransient<IValidator<CreateClientModel>, CreateClientValidator>();
            services.AddTransient<IValidator<AlterClientModel>, UpdateClientValidator>();

            return services;
        }
    }
}
