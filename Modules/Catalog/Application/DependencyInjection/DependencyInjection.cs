using Contract.Cart.Abstractions;
using Contract.Order.Abstractions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Modules.Catalog.Application.Services;
using MiniECommerce.Modules.Catalog.Application.Services.Abstractions;

namespace MiniECommerce.Modules.Catalog.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCatalogApplication(
            this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;
            services.AddAutoMapper(cfg => { }, assembly);
            services.AddValidatorsFromAssembly(assembly);

            services.AddScoped<IProductCatalog, ProductService>();
            services.AddScoped<IOrderCatalog, ProductService>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            return services;
        }
    }
}
