using AppointmentSystem.Domain.Common;

namespace AppointmentSystem.Domain.Entities;

public class BranchHoliday : BaseEntity
{
    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = null!;
    public DateTime Date { get; set; }
    public string? Description { get; set; }
}

public class ProfessionalAbsence : BaseEntity
{
    public Guid ProfessionalId { get; set; }
    public Professional Professional { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Reason { get; set; }
}
