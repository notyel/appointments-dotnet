using AppointmentSystem.Domain.Common;

namespace AppointmentSystem.Domain.Entities;

public class BranchSchedule : BaseEntity
{
    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = null!;

    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan OpenTime { get; set; }
    public TimeSpan CloseTime { get; set; }
    public bool IsClosed { get; set; } = false;
}

public class ProfessionalSchedule : BaseEntity
{
    public Guid ProfessionalId { get; set; }
    public Professional Professional { get; set; } = null!;

    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsOff { get; set; } = false;
}
