using AppointmentSystem.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AppointmentSystem.Infrastructure.Settings;

/// <summary>
/// Implementación concreta que lee la configuración del tenant y el formato regional
/// desde <c>appsettings.json</c> sección <c>AppConfig</c>.
/// </summary>
internal sealed class AppSettings : ITenantSettings, IFormatSettings
{
    public Guid BusinessId { get; }
    public string CurrencyCode { get; }
    public string CurrencySymbol { get; }
    public string CultureInfo { get; }
    public string TimeZoneId { get; }

    public AppSettings(IConfiguration configuration)
    {
        var section = configuration.GetSection("AppConfig");

        BusinessId = Guid.Parse(section["BusinessId"]
            ?? throw new InvalidOperationException("AppConfig:BusinessId no está configurado en appsettings.json"));

        CurrencyCode   = section["CurrencyCode"]   ?? "USD";
        CurrencySymbol = section["CurrencySymbol"] ?? "$";
        CultureInfo    = section["CultureInfo"]    ?? "en-US";
        TimeZoneId     = section["TimeZoneId"]     ?? "UTC";
    }
}
