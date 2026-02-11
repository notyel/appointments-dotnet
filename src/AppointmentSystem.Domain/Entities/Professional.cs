using AppointmentSystem.Domain.Common;

namespace AppointmentSystem.Domain.Entities;

public class Professional : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public string? ImageUrl { get; set; }

    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = null!;

    public ICollection<ProfessionalSchedule> Schedules { get; set; } = new List<ProfessionalSchedule>();
    public ICollection<ProfessionalAbsence> Absences { get; set; } = new List<ProfessionalAbsence>();
    public ICollection<ProfessionalService> ProfessionalServices { get; set; } = new List<ProfessionalService>();
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}

public class ProfessionalService : BaseEntity
{
    public Guid ProfessionalId { get; set; }
    public Professional Professional { get; set; } = null!;

    public Guid BranchServiceId { get; set; }
    public BranchService BranchService { get; set; } = null!;
}
