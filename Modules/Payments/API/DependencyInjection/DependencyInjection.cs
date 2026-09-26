using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Modules.Payment.Application.DependencyInjection;
using MiniECommerce.Modules.Payment.Infrastructure.DependencyInjection;

namespace MiniECommerce.Modules.Payments.API.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPaymentsModule(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddPaymentApplication();
            services.AddPaymentInfrastructure(configuration);
            return services;
        }
    }
}
