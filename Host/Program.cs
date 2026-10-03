using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using MiniECommerce.Modules.Cart.API.Controllers;
using MiniECommerce.Modules.Cart.API.DependencyInjection;
using MiniECommerce.Modules.Catalog.API.Controllers;
using MiniECommerce.Modules.Catalog.Application.DependencyInjection;
using MiniECommerce.Modules.Catalog.Infrastructure.DependencyInjection;
using MiniECommerce.Modules.Identity.API.Controllers;
using MiniECommerce.Modules.Identity.Application.DependencyInjection;
using MiniECommerce.Modules.Identity.Infrastructure.DependencyInjection;
using MiniECommerce.Modules.Identity.Infrastructure.Security;
using MiniECommerce.Modules.Inventory.API.Controllers;
using MiniECommerce.Modules.Inventory.Application.DependencyInjection;
using MiniECommerce.Modules.Inventory.Infrastructure.DependencyInjection;
using MiniECommerce.Modules.Notifications.API.Controllers;
using MiniECommerce.Modules.Notifications.API.Hubs;
using MiniECommerce.Modules.Notifications.API.Realtime;
using MiniECommerce.Modules.Notifications.Application.Abstractions;
using MiniECommerce.Modules.Notifications.Application.DependencyInjection;
using MiniECommerce.Modules.Notifications.Infrastructure.DependencyInjection;
using MiniECommerce.Modules.Orders.API.Controllers;
using MiniECommerce.Modules.Orders.API.DependencyInjection;
using MiniECommerce.Modules.Payment.API.Controllers;
using MiniECommerce.Modules.Payments.API.DependencyInjection;
using MiniECommerce.Modules.Reviews.API.Controllers;
using MiniECommerce.Modules.Reviews.Application.DependencyInjection;
using MiniECommerce.Modules.Reviews.Infrastructure.DependencyInjection;
using System.Text;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddIdentityInfrastructure(
    builder.Configuration);
builder.Services.AddIdentityApplication();
builder.Services.AddReviewApplictaion();
builder.Services.AddReviewInfrastructure(builder.Configuration);
builder.Services.AddNotificationInfrastructure(builder.Configuration);
builder.Services.AddNotificationApplication();
builder.Services.AddCartModule(builder.Configuration);
builder.Services.AddOrdersModule(builder.Configuration);
builder.Services.AddPaymentsModule(builder.Configuration);
builder.Services.AddCatalogInfrastructure(builder.Configuration);
builder.Services.AddCatalogApplication();
builder.Services.AddInventoryInfrastructure(builder.Configuration);
builder.Services.AddInventoryApplication();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSignalR();
builder.Services.AddSingleton<IUserIdProvider, JwtUserIdProvider>();
builder.Services.AddScoped<
    INotificationRealtimePublisher,
    NotificationRealtimePublisher>();
builder.Services.AddControllers()
    .AddApplicationPart(typeof(InventoryController).Assembly)
    .AddApplicationPart(typeof(CartController).Assembly)
    .AddApplicationPart(typeof(OrderController).Assembly)
    .AddApplicationPart(typeof(PaymentController).Assembly)
    .AddApplicationPart(typeof(AuthController).Assembly)
    .AddApplicationPart(typeof(ProductController).Assembly)
    .AddApplicationPart(typeof(NotificationsController).Assembly)
    .AddApplicationPart(typeof(ReviewsController).Assembly);
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token."
    });

    options.AddSecurityRequirement(document =>
    {
        var scheme = new OpenApiSecuritySchemeReference("Bearer", document);

        return new OpenApiSecurityRequirement
        {
            [scheme] = new List<string>()
        };
    });
});
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.MapInboundClaims = false;

        var jwtOptions = builder.Configuration
            .GetSection("Jwt")
            .Get<JwtOptions>()!;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtOptions.Issuer,

            ValidateAudience = true,
            ValidAudience = jwtOptions.Audience,

            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.MapHub<NotificationHub>("/hubs/notifications");

app.Run();
public partial class Program { }