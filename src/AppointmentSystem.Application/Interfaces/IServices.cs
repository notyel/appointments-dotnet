using AppointmentSystem.Application.DTOs;

namespace AppointmentSystem.Application.Interfaces;

public interface IAppointmentService
{
    Task<IEnumerable<AppointmentDto>> GetBranchAppointmentsAsync(Guid branchId, DateTime date);
    Task<Guid> CreateAppointmentAsync(CreateAppointmentDto dto);
    Task CancelAppointmentAsync(Guid appointmentId);
    Task<IEnumerable<AppointmentDto>> GetClientAppointmentsAsync(string email);
}

public interface IBranchManagementService
{
    Task<IEnumerable<BranchDto>> GetAllBranchesAsync();
    Task<BranchDto?> GetBranchByIdAsync(Guid id);
    Task<IEnumerable<ServiceDto>> GetServicesByBranchAsync(Guid branchId);
    Task<IEnumerable<ProfessionalDto>> GetProfessionalsByServiceAsync(Guid branchServiceId);
    Task<IEnumerable<DateTime>> GetAvailableSlotsAsync(Guid branchServiceId, Guid professionalId, DateTime date);
}
