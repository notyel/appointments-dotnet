using AppointmentSystem.Application.DTOs;
using AppointmentSystem.Application.Interfaces;
using AppointmentSystem.Domain.Entities;
using AppointmentSystem.Domain.Interfaces;
using AppointmentSystem.Domain.Enums;

namespace AppointmentSystem.Application.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly INotificationService _notificationService;

    public AppointmentService(IUnitOfWork unitOfWork, INotificationService notificationService)
    {
        _unitOfWork = unitOfWork;
        _notificationService = notificationService;
    }

    public async Task<IEnumerable<AppointmentDto>> GetBranchAppointmentsAsync(Guid branchId, DateTime date)
    {
        var dateUtc = DateTime.SpecifyKind(date.Date, DateTimeKind.Utc);
        var nextDayUtc = dateUtc.AddDays(1);
        
        var appointments = await _unitOfWork.Repository<Appointment>()
            .FindAsync(a => a.BranchId == branchId 
                && a.StartTime >= dateUtc 
                && a.StartTime < nextDayUtc,
                a => a.BranchService.Service,
                a => a.Professional);

        return appointments.Select(a => new AppointmentDto {
            Id = a.Id,
            BranchName = "",
            ServiceName = a.BranchService.Service.Name,
            ProfessionalName = $"{a.Professional.FirstName} {a.Professional.LastName}",
            StartTime = a.StartTime,
            EndTime = a.EndTime,
            Status = a.Status.ToString()
        });
    }

    public async Task<Guid> CreateAppointmentAsync(CreateAppointmentDto dto)
    {
        var bs = await _unitOfWork.Repository<BranchService>().GetByIdAsync(dto.BranchServiceId);
        if (bs == null) throw new Exception("Service not found");

        // Asegurar que el DateTime esté en UTC
        var startTimeUtc = dto.StartTime.Kind == DateTimeKind.Utc 
            ? dto.StartTime 
            : DateTime.SpecifyKind(dto.StartTime, DateTimeKind.Utc);
        
        var endTime = startTimeUtc.AddMinutes(bs.DurationMinutes);

        // Correct Overlap Check
        var overlaps = await _unitOfWork.Repository<Appointment>().FindAsync(a =>
            a.ProfessionalId == dto.ProfessionalId &&
            a.Status != AppointmentStatus.Cancelled &&
            startTimeUtc < a.EndTime && endTime > a.StartTime);

        if (overlaps.Any())
        {
            throw new Exception("El profesional seleccionado ya tiene una cita en ese horario.");
        }

        // Branch Capacity Check
        var branch = await _unitOfWork.Repository<Branch>().GetByIdAsync(dto.BranchId);
        if (branch != null)
        {
            var branchAppointments = await _unitOfWork.Repository<Appointment>().FindAsync(a =>
                a.BranchId == dto.BranchId &&
                a.Status != AppointmentStatus.Cancelled &&
                startTimeUtc < a.EndTime && endTime > a.StartTime);

            if (branchAppointments.Count() >= branch.SimultaneousCapacity)
            {
                throw new Exception("La sucursal ha alcanzado su capacidad máxima para este horario.");
            }
        }

        // Find or create client
        var clients = await _unitOfWork.Repository<Client>().FindAsync(c => c.Email == dto.ClientEmail);
        var client = clients.FirstOrDefault();
        if (client == null)
        {
            client = new Client
            {
                FirstName = dto.ClientFirstName,
                LastName = dto.ClientLastName,
                Email = dto.ClientEmail,
                Phone = dto.ClientPhone
            };
            await _unitOfWork.Repository<Client>().AddAsync(client);
        }

        var appointment = new Appointment
        {
            BranchId = dto.BranchId,
            BranchServiceId = dto.BranchServiceId,
            ProfessionalId = dto.ProfessionalId,
            ClientId = client.Id,
            StartTime = startTimeUtc,
            EndTime = endTime,
            Status = AppointmentStatus.Created,
            Notes = dto.Notes
        };

        await _unitOfWork.Repository<Appointment>().AddAsync(appointment);
        await _unitOfWork.SaveChangesAsync();

        await _notificationService.LogNotificationAsync(appointment.Id, client.Email, "Confirmation", $"Cita confirmada para el {appointment.StartTime}");

        return appointment.Id;
    }

    public async Task CancelAppointmentAsync(Guid appointmentId)
    {
        var appointment = await _unitOfWork.Repository<Appointment>().GetByIdAsync(appointmentId);
        if (appointment == null) return;

        // Cancellation Policy: 24 hours advance
        if (appointment.StartTime < DateTime.UtcNow.AddHours(24))
        {
            throw new Exception("No se puede cancelar la cita con menos de 24 horas de antelación.");
        }

        appointment.Status = AppointmentStatus.Cancelled;
        await _unitOfWork.Repository<Appointment>().UpdateAsync(appointment);
        await _unitOfWork.SaveChangesAsync();

        var client = await _unitOfWork.Repository<Client>().GetByIdAsync(appointment.ClientId);
        if (client != null)
        {
            await _notificationService.LogNotificationAsync(appointment.Id, client.Email, "Cancellation", $"Su cita para el {appointment.StartTime} ha sido cancelada.");
        }
    }

    public async Task<IEnumerable<AppointmentDto>> GetClientAppointmentsAsync(string email)
    {
        var clients = await _unitOfWork.Repository<Client>().FindAsync(c => c.Email == email);
        var client = clients.FirstOrDefault();
        if (client == null) return Enumerable.Empty<AppointmentDto>();

        var appointments = await _unitOfWork.Repository<Appointment>()
            .FindAsync(a => a.ClientId == client.Id,
                a => a.BranchService.Service,
                a => a.Professional,
                a => a.Branch);

        return appointments.Select(a => new AppointmentDto
        {
            Id = a.Id,
            BranchName = a.Branch.Name,
            ServiceName = a.BranchService.Service.Name,
            ProfessionalName = $"{a.Professional.FirstName} {a.Professional.LastName}",
            StartTime = a.StartTime,
            EndTime = a.EndTime,
            Status = a.Status.ToString()
        });
    }
}
