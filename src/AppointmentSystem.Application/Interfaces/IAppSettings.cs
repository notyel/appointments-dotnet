namespace AppointmentSystem.Application.Interfaces;

/// <summary>
/// Configuración de tenant: identifica qué Business está activo.
/// Todos los accesos a datos deben filtrarse por <see cref="BusinessId"/>.
/// </summary>
public interface ITenantSettings
{
    Guid BusinessId { get; }
}

/// <summary>
/// Configuración regional para formateo de moneda, fechas y zona horaria.
/// </summary>
public interface IFormatSettings
{
    /// <summary>Código ISO 4217 (ej: COP, USD).</summary>
    string CurrencyCode { get; }

    /// <summary>Símbolo visual (ej: $, €).</summary>
    string CurrencySymbol { get; }

    /// <summary>Cultura .NET para formateo (ej: es-CO).</summary>
    string CultureInfo { get; }

    /// <summary>IANA Time Zone ID (ej: America/Bogota).</summary>
    string TimeZoneId { get; }
}
