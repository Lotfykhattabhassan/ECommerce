using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Modules.Inventory.Application.DependencyInjection;
using MiniECommerce.Modules.Inventory.Infrastructure.DependencyInjection;

namespace MiniECommerce.Modules.Inventory.API.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInventoryModule(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddInventoryApplication();
            services.AddInventoryInfrastructure(configuration);
            return services;
        }
    }
}
