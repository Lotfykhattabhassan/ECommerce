using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Modules.Reviews.Application.Abstractions;
using MiniECommerce.Modules.Reviews.Infrastructure.Persistence;
using MiniECommerce.Modules.Reviews.Infrastructure.Repositories;

namespace MiniECommerce.Modules.Reviews.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddReviewInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MiniECommerce");
            services.AddDbContext<ReviewDbContext>(opt => opt.UseSqlServer(connectionString));

            services.AddScoped<IReviewRepository, ReviewRepository>(); 
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
