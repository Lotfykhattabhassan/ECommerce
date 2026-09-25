using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Modules.Orders.Application.Services;
using MiniECommerce.Modules.Orders.Application.Services.Abstractions;

namespace MiniECommerce.Modules.Orders.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOrdersApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;
            services.AddAutoMapper(cfg => { }, assembly);
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderItemService, OrderItemService>();
            return services;
        }
    }
}
