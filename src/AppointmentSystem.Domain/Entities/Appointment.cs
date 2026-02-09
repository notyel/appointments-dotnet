using AppointmentSystem.Domain.Common;
using AppointmentSystem.Domain.Enums;

namespace AppointmentSystem.Domain.Entities;

public class Appointment : BaseEntity
{
    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = null!;

    public Guid BranchServiceId { get; set; }
    public BranchService BranchService { get; set; } = null!;

    public Guid ProfessionalId { get; set; }
    public Professional Professional { get; set; } = null!;

    public Guid ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public AppointmentStatus Status { get; set; } = AppointmentStatus.Created;
    public string? Notes { get; set; }
}
