using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Modules.Identity.Application.Services;
using MiniECommerce.Modules.Identity.Application.Services.Abstractions;

namespace MiniECommerce.Modules.Identity.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIdentityApplication(
            this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddValidatorsFromAssembly(assembly);

            services.AddAutoMapper(cfg => { }, assembly);


            return services;
        }
    }
}

