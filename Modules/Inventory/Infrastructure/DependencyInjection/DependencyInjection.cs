using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Modules.Inventory.Application.Abstractions;
using MiniECommerce.Modules.Inventory.Infrastructure.Persistence;
using MiniECommerce.Modules.Inventory.Infrastructure.Repositories;

namespace MiniECommerce.Modules.Inventory.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInventoryInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MiniECommerce");

            services.AddDbContext<InventoryDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductInventoryRepository, ProductInventoryRepository>();

            return services;
        }
    }
}
