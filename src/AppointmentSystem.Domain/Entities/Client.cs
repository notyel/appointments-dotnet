using AppointmentSystem.Domain.Common;

namespace AppointmentSystem.Domain.Entities;

public class Client : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    // Identity UserId if registered
    public string? UserId { get; set; }

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
