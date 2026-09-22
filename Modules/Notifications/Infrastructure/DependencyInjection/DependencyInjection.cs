using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Modules.Notifications.Application.Abstractions;
using MiniECommerce.Modules.Notifications.Infrastructure.Persistence;
using MiniECommerce.Modules.Notifications.Infrastructure.Repositories;

namespace MiniECommerce.Modules.Notifications.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddNotificationInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MiniECommerce");
            services.AddDbContext<NotificationDbContext>(opt => opt.UseSqlServer(connectionString));

            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<INotificationRepository, NotificationRepository>();

            return services;
        }
    }
}
