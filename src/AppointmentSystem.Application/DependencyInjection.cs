using Microsoft.Extensions.DependencyInjection;
using AppointmentSystem.Application.Interfaces;
using AppointmentSystem.Application.Services;

namespace AppointmentSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IBranchManagementService, BranchManagementService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<INotificationService, NotificationService>();

        return services;
    }
}
