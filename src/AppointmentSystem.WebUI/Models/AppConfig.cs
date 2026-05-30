namespace AppointmentSystem.WebUI.Models;

public class AppConfig
{
    /// <summary>Id del tenant (Business) activo. Todos los datos se filtran por este valor.</summary>
    public Guid BusinessId { get; set; }

    public string BusinessName { get; set; } = string.Empty;

    /// <summary>Código ISO 4217 de la moneda (ej: COP, USD, EUR).</summary>
    public string CurrencyCode { get; set; } = "USD";

    /// <summary>Símbolo visual de la moneda (ej: $, €, £).</summary>
    public string CurrencySymbol { get; set; } = "$";

    /// <summary>Número de decimales a mostrar en valores monetarios (ej: 0 para COP, 2 para USD).</summary>
    public int CurrencyDecimals { get; set; } = 2;

    /// <summary>Cultura .NET para formateo de números y fechas (ej: es-CO, en-US).</summary>
    public string CultureInfo { get; set; } = "en-US";

    /// <summary>IANA Time Zone ID para conversión de fechas (ej: America/Bogota, UTC).</summary>
    public string TimeZoneId { get; set; } = "UTC";

    /// <summary>Número máximo de días a futuro que se permite reservar una cita.</summary>
    public int MaxBookingDays { get; set; } = 30;

    public ContactInfo Contact { get; set; } = new();
}

public class ContactInfo
{
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}
