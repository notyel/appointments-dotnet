namespace AppointmentSystem.Application.Interfaces;

public interface INotificationService
{
    Task LogNotificationAsync(Guid? appointmentId, string recipient, string eventType, string content);
}
