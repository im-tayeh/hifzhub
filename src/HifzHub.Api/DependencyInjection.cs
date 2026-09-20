using HifzHub.Api.Services;
using HifzHub.Application.Abstractions;
using Microsoft.OpenApi;



namespace HifzHub.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddHttpContextAccessor();
        services.AddScoped<ITenantContext, TenantContext>();

        services.AddOpenApi();

        return services;
    }
}