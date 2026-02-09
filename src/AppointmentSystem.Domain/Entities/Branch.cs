using AppointmentSystem.Domain.Common;

namespace AppointmentSystem.Domain.Entities;

public class Branch : BaseEntity
{
    public Guid BusinessId { get; set; }
    public Business Business { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string TimeZone { get; set; } = "UTC";
    public bool IsActive { get; set; } = true;
    public int SimultaneousCapacity { get; set; } = 10; // Default

    public ICollection<BranchSchedule> Schedules { get; set; } = new List<BranchSchedule>();
    public ICollection<BranchHoliday> Holidays { get; set; } = new List<BranchHoliday>();
    public ICollection<Professional> Professionals { get; set; } = new List<Professional>();
    public ICollection<BranchService> BranchServices { get; set; } = new List<BranchService>();
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
