using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Modules.Payment.Application.Abstractions;
using MiniECommerce.Modules.Payment.Application.Services;

namespace MiniECommerce.Modules.Payment.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPaymentApplication(
            this IServiceCollection services)
        {
            services.AddScoped<IPaymentService, PaymentService>();

            services.AddValidatorsFromAssembly(
                typeof(DependencyInjection).Assembly);

            services.AddAutoMapper( cfg => { },
                typeof(DependencyInjection).Assembly);

            return services;
        }
    }
}