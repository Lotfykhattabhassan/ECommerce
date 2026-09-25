
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Modules.Orders.Application.DependencyInjection;
using MiniECommerce.Modules.Orders.Infrastructure.DependencyInjection;

namespace MiniECommerce.Modules.Orders.API.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOrdersModule(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddOrdersApplication();
            services.AddOrdersInfrastructure(configuration);
            return services;
        }
    }
}
