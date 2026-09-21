using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Modules.Reviews.Application.Services;
using MiniECommerce.Modules.Reviews.Application.Services.Abstractions;

namespace MiniECommerce.Modules.Reviews.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddReviewApplictaion(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;
            services.AddScoped<IReviewService, ReviewService>();

            services.AddValidatorsFromAssembly(assembly);

            services.AddAutoMapper(cfg => { }, assembly);

            return services;
        }
    }
}
