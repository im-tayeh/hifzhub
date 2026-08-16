using HifzHub.Application.Auth;
using HifzHub.Application.Stages;
using Microsoft.Extensions.DependencyInjection;

namespace HifzHub.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<LoginHandler>();
        services.AddScoped<StageService>();

        return services;
    }
}