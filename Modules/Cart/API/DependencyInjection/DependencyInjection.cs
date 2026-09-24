using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Modules.Cart.Infrastructure.DependencyInjection;
using MiniECommerce.Modules.Cart.Application.DependencyInjection;

namespace MiniECommerce.Modules.Cart.API.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCartModule(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddCartInfrastructure(configuration);
            services.AddCartApplication();
            return services;
        }
    }
}
