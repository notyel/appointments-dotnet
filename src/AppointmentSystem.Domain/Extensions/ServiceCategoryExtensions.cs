using AppointmentSystem.Domain.Enums;

namespace AppointmentSystem.Domain.Extensions;

public static class ServiceCategoryExtensions
{
    public static string ToSpanish(this ServiceCategory category) => category switch
    {
        ServiceCategory.Hairdressing => "Peluquería",
        ServiceCategory.Aesthetics   => "Estética",
        ServiceCategory.SPA          => "SPA",
        ServiceCategory.Other        => "Otros",
        _                            => category.ToString()
    };
}
