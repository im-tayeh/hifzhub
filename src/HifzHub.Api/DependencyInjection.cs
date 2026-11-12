using HifzHub.Api.Services;
using HifzHub.Application.Abstractions;
using HifzHub.Domain.Enums;
using Microsoft.OpenApi;



namespace HifzHub.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddHttpContextAccessor();
        services.AddOpenApi();

        services.AddScoped<ITenantContext, TenantContext>();
        services.AddScoped<ICurrentUser, CurrentUser>();

        services.AddAuthorizationBuilder()
            .AddPolicy("CenterManager", policy =>
            policy.RequireRole(
                nameof(RoleType.CenterAdmin),
                nameof(RoleType.CenterAmir),
                nameof(RoleType.DeputyAmir)))
            .AddPolicy("SuperAdmin", policy =>
            policy.RequireRole(nameof(RoleType.SuperAdmin)));

        return services;
    }
}