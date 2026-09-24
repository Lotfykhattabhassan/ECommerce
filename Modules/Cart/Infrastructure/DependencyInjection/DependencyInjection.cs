using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Modules.Cart.Application.Abstractions;
using MiniECommerce.Modules.Cart.Infrastructure.Persistence;
using MiniECommerce.Modules.Cart.Infrastructure.Repositories;

namespace MiniECommerce.Modules.Cart.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCartInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MiniECommerce");
            services.AddDbContext<CartDbContext>(opt => opt.UseSqlServer(connectionString));

            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<ICartItemRepository, CartItemRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
