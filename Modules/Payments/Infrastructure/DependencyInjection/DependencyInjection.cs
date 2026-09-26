using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Modules.Payment.Application.Abstractions;
using MiniECommerce.Modules.Payment.Application.Abstractions.Persistence;
using MiniECommerce.Modules.Payment.Infrastructure.Persistence;
using MiniECommerce.Modules.Payment.Infrastructure.Repositories;

namespace MiniECommerce.Modules.Payment.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPaymentInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString =
                configuration.GetConnectionString("MiniECommerce");

            services.AddDbContext<PaymentDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}