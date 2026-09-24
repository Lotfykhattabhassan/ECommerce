using Contract.Cart.Abstractions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Modules.Inventory.Application.Services;
using MiniECommerce.Modules.Inventory.Application.Services.Abstractions;

namespace MiniECommerce.Modules.Inventory.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInventoryApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;
            services.AddValidatorsFromAssembly(assembly);

            services.AddScoped<IInventoryService, InventoryService>();

            services.AddScoped<IProductInventory, InventoryService>();
            return services;
        }
    }
}
