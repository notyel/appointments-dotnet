using AppointmentSystem.Domain.Common;

namespace AppointmentSystem.Domain.Entities;

public class Notification : BaseEntity
{
    public Guid? AppointmentId { get; set; }
    public string Recipient { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; // Email, SMS, etc.
    public string Event { get; set; } = string.Empty; // Confirmation, Reminder, Cancellation
    public string Content { get; set; } = string.Empty;
    public bool IsSent { get; set; } = false;
    public DateTime? SentAt { get; set; }
}
