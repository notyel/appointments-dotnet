using AppointmentSystem.Application.Interfaces;
using AppointmentSystem.Domain.Entities;
using AppointmentSystem.Domain.Interfaces;

namespace AppointmentSystem.Application.Services;

public class NotificationService : INotificationService
{
    private readonly IUnitOfWork _unitOfWork;

    public NotificationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task LogNotificationAsync(Guid? appointmentId, string recipient, string eventType, string content)
    {
        var notification = new Notification
        {
            AppointmentId = appointmentId,
            Recipient = recipient,
            Type = "Email", // Default for now
            Event = eventType,
            Content = content,
            IsSent = true, // Simulated
            SentAt = DateTime.UtcNow
        };

        await _unitOfWork.Repository<Notification>().AddAsync(notification);
        await _unitOfWork.SaveChangesAsync();
    }
}
