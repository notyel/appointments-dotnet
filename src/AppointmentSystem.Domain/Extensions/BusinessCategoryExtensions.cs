using AppointmentSystem.Domain.Enums;

namespace AppointmentSystem.Domain.Extensions;

/// <summary>
/// Extensiones para el enum <see cref="BusinessCategory"/>.
/// Provee transformaciones de presentación, como la traducción al español.
/// </summary>
public static class BusinessCategoryExtensions
{
    public static string ToSpanish(this BusinessCategory category) => category switch
    {
        BusinessCategory.Hairdressing => "Peluquería",
        BusinessCategory.Aesthetics   => "Estética",
        BusinessCategory.SPA          => "SPA",
        BusinessCategory.Other        => "Otros",
        _                             => category.ToString()
    };
}
