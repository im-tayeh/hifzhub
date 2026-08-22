using HifzHub.Application.Auth;
using HifzHub.Application.Halaqat;
using HifzHub.Application.Stages;
using HifzHub.Application.Students;
using Microsoft.Extensions.DependencyInjection;

namespace HifzHub.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<LoginHandler>();
        services.AddScoped<StageService>();
        services.AddScoped<HalaqaService>();
        services.AddScoped<StudentService>();
        services.AddScoped<HifzHub.Application.Programs.ProgramService>();
        services.AddScoped<HifzHub.Application.Attendances.AttendanceService>();
        services.AddScoped<HifzHub.Application.Recitations.RecitationService>();

        return services;
    }
}