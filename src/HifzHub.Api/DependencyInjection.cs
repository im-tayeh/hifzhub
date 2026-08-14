using HifzHub.Api.Services;
using HifzHub.Application.Abstractions;

namespace HifzHub.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddOpenApi();

        services.AddHttpContextAccessor();
        services.AddScoped<ITenantContext, TenantContext>();

        return services;
    }
}