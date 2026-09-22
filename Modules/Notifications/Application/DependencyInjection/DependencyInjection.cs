using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Modules.Notifications.Application.Services;
using MiniECommerce.Modules.Notifications.Application.Services.Abstraction;

namespace MiniECommerce.Modules.Notifications.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddNotificationApplication(
            this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;
            services.AddScoped<INotificationService, NotificationService>();

            services.AddAutoMapper(cfg => { }, assembly);
            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}
