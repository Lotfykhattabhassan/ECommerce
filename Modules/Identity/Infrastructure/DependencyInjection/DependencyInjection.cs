using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Modules.Identity.Application.Abstractions.Persistence;
using MiniECommerce.Modules.Identity.Application.Abstractions.Security;
using MiniECommerce.Modules.Identity.Infrastructure.Persistence;
using MiniECommerce.Modules.Identity.Infrastructure.Repositories;
using MiniECommerce.Modules.Identity.Infrastructure.Security;

namespace MiniECommerce.Modules.Identity.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIdentityInfrastructure(
            this IServiceCollection services,IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MiniECommerce");
            services.AddDbContext<IdentityDbContext>(opt => opt.UseSqlServer(connectionString));

            services.Configure<JwtOptions>(
                 configuration.GetSection(JwtOptions.SectionName));

            services.AddHttpContextAccessor();

            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserCredentialRepository, UserCredentialRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICurrentUser, CurrentUser>();

            return services;
        }
    }
}
