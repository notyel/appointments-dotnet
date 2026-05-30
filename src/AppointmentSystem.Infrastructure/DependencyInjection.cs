using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AppointmentSystem.Infrastructure.Persistence;
using AppointmentSystem.Infrastructure.Persistence.Repositories;
using AppointmentSystem.Infrastructure.Settings;
using AppointmentSystem.Domain.Interfaces;
using AppointmentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AppointmentSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Registrar configuración de tenant y formato como singleton (no cambia en runtime)
        services.AddSingleton<AppSettings>();
        services.AddSingleton<ITenantSettings>(sp => sp.GetRequiredService<AppSettings>());
        services.AddSingleton<IFormatSettings>(sp => sp.GetRequiredService<AppSettings>());

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddIdentity<IdentityUser, IdentityRole>(options => {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
