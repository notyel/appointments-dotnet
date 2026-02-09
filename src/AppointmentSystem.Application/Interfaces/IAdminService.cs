using AppointmentSystem.Domain.Entities;

namespace AppointmentSystem.Application.Interfaces;

public interface IAdminService
{
    // Business & Branch
    Task<Guid> CreateBranchAsync(Branch branch);
    Task UpdateBranchAsync(Branch branch);

    // Services
    Task<Guid> CreateServiceAsync(Service service);
    Task AddServiceToBranchAsync(Guid branchId, Guid serviceId, int duration, decimal price);

    // Professionals
    Task<Guid> CreateProfessionalAsync(Professional professional);
    Task AssignProfessionalToServiceAsync(Guid professionalId, Guid branchServiceId);
}
