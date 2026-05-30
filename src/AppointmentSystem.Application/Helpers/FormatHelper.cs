using System.Globalization;
using AppointmentSystem.Application.Interfaces;

namespace AppointmentSystem.Application.Helpers;

/// <summary>
/// Utilidades para formatear moneda y fechas respetando la configuración regional del tenant.
/// </summary>
public static class FormatHelper
{
    /// <summary>
    /// Formatea un monto como moneda según la cultura configurada.
    /// Ej: 25000 → "$25.000" con es-CO / COP
    /// </summary>
    public static string FormatCurrency(decimal amount, IFormatSettings settings)
    {
        var culture = new CultureInfo(settings.CultureInfo);
        culture.NumberFormat.CurrencySymbol = settings.CurrencySymbol;
        culture.NumberFormat.CurrencyDecimalDigits = settings.CurrencyDecimals;
        return amount.ToString("C", culture);
    }

    /// <summary>
    /// Convierte un <see cref="DateTime"/> UTC a la zona horaria del tenant y lo formatea.
    /// </summary>
    public static string FormatDateTime(DateTime utcDateTime, IFormatSettings settings, string format = "dddd, d 'de' MMMM 'de' yyyy - HH:mm")
    {
        var tz = TimeZoneInfo.FindSystemTimeZoneById(settings.TimeZoneId);
        var local = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, tz);
        return local.ToString(format, new CultureInfo(settings.CultureInfo));
    }

    /// <summary>
    /// Convierte un <see cref="DateTime"/> UTC a la zona horaria del tenant.
    /// </summary>
    public static DateTime ToLocalTime(DateTime utcDateTime, IFormatSettings settings)
    {
        var tz = TimeZoneInfo.FindSystemTimeZoneById(settings.TimeZoneId);
        return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, tz);
    }

    /// <summary>
    /// Convierte hora local del tenant a UTC.
    /// </summary>
    public static DateTime ToUtc(DateTime localDateTime, IFormatSettings settings)
    {
        var tz = TimeZoneInfo.FindSystemTimeZoneById(settings.TimeZoneId);
        return TimeZoneInfo.ConvertTimeToUtc(localDateTime, tz);
    }
}
