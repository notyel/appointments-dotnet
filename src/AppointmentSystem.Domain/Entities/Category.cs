using AppointmentSystem.Domain.Common;
using AppointmentSystem.Domain.Enums;

namespace AppointmentSystem.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ServiceCategory ServiceCategory { get; set; }

    public ICollection<Service> Services { get; set; } = new List<Service>();
}
