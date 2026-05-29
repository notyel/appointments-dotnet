using AppointmentSystem.Domain.Common;
using AppointmentSystem.Domain.Enums;

namespace AppointmentSystem.Domain.Entities;

/// <summary>
/// Entidad persistida que representa una categoría de negocio para agrupar servicios.
/// El campo <see cref="BusinessCategory"/> vincula esta entidad con el enum de clasificación.
/// </summary>
public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    /// <summary>Clasificación de negocio que representa esta categoría.</summary>
    public BusinessCategory BusinessCategory { get; set; }

    public ICollection<Service> Services { get; set; } = new List<Service>();
}
