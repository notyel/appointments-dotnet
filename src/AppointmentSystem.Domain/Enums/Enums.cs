namespace AppointmentSystem.Domain.Enums;

public enum AppointmentStatus
{
    Created,
    Confirmed,
    Cancelled,
    Rescheduled,
    Attended,
    NoShow
}

/// <summary>
/// Clasificación de negocio de los servicios ofrecidos.
/// Este enum representa las categorías del modelo de negocio y se almacena
/// como columna en la tabla Services. No confundir con la entidad <see cref="AppointmentSystem.Domain.Entities.Category"/>,
/// que es la representación persistida y navegable de esta clasificación.
/// </summary>
public enum BusinessCategory
{
    Hairdressing,
    Aesthetics,
    SPA,
    Other
}
