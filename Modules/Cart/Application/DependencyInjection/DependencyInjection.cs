using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Modules.Cart.Application.Services;
using MiniECommerce.Modules.Cart.Application.Services.Abstractions;

namespace MiniECommerce.Modules.Cart.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCartApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;
            services.AddAutoMapper(cfg => { }, assembly);
            services.AddValidatorsFromAssembly(assembly);
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICartItemService, CartItemService>();
            return services;
        }
    }
}
