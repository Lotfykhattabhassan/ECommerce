using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Modules.Orders.Application.Abstractions;
using MiniECommerce.Modules.Orders.Infrastructure.Persistence;
using MiniECommerce.Modules.Orders.Infrastructure.Repositories;

namespace MiniECommerce.Modules.Orders.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOrdersInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MiniECommerce");

            services.AddDbContext<OrderDbContext>(opt => opt.UseSqlServer(connectionString));

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}