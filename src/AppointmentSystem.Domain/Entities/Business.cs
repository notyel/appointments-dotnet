using AppointmentSystem.Domain.Common;

namespace AppointmentSystem.Domain.Entities;

public class Business : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? LogoUrl { get; set; }
    public ICollection<Branch> Branches { get; set; } = new List<Branch>();
}
