using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Modules.Catalog.Application.Abstractions;
using MiniECommerce.Modules.Catalog.Infrastructure.Persistence;
using MiniECommerce.Modules.Catalog.Infrastructure.Repositories;

namespace MiniECommerce.Modules.Catalog.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCatalogInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MiniECommerce");
            services.AddDbContext<CatalogDbContext>(opt => opt.UseSqlServer(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            return services;
        }
    }
}
