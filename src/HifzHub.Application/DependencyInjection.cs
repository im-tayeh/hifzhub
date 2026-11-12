using HifzHub.Application.Attendances;
using HifzHub.Application.Auth;
using HifzHub.Application.Halaqat;
using HifzHub.Application.Programs;
using HifzHub.Application.Recitations;
using HifzHub.Application.Staff;
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
        services.AddScoped<ProgramService>();
        services.AddScoped<AttendanceService>();
        services.AddScoped<RecitationService>();
        services.AddScoped<StaffProfileService>();

        return services;
    }
}