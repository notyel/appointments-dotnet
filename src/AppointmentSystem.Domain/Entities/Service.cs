using AppointmentSystem.Domain.Common;
using AppointmentSystem.Domain.Enums;

namespace AppointmentSystem.Domain.Entities;

public class Service : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ServiceCategory Category { get; set; }
    public string? ImageUrl { get; set; }

    public ICollection<BranchService> BranchServices { get; set; } = new List<BranchService>();
}

public class BranchService : BaseEntity
{
    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = null!;

    public Guid ServiceId { get; set; }
    public Service Service { get; set; } = null!;

    public int DurationMinutes { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<ProfessionalService> ProfessionalServices { get; set; } = new List<ProfessionalService>();
}
