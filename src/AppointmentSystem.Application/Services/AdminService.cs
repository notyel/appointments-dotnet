using AppointmentSystem.Application.Interfaces;
using AppointmentSystem.Domain.Entities;
using AppointmentSystem.Domain.Interfaces;

namespace AppointmentSystem.Application.Services;

public class AdminService : IAdminService
{
    private readonly IUnitOfWork _unitOfWork;

    public AdminService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> CreateBranchAsync(Branch branch)
    {
        await _unitOfWork.Repository<Branch>().AddAsync(branch);
        await _unitOfWork.SaveChangesAsync();
        return branch.Id;
    }

    public async Task UpdateBranchAsync(Branch branch)
    {
        await _unitOfWork.Repository<Branch>().UpdateAsync(branch);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Guid> CreateServiceAsync(Service service)
    {
        await _unitOfWork.Repository<Service>().AddAsync(service);
        await _unitOfWork.SaveChangesAsync();
        return service.Id;
    }

    public async Task AddServiceToBranchAsync(Guid branchId, Guid serviceId, int duration, decimal price)
    {
        var branchService = new BranchService
        {
            BranchId = branchId,
            ServiceId = serviceId,
            DurationMinutes = duration,
            Price = price
        };
        await _unitOfWork.Repository<BranchService>().AddAsync(branchService);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Guid> CreateProfessionalAsync(Professional professional)
    {
        await _unitOfWork.Repository<Professional>().AddAsync(professional);
        await _unitOfWork.SaveChangesAsync();
        return professional.Id;
    }

    public async Task AssignProfessionalToServiceAsync(Guid professionalId, Guid branchServiceId)
    {
        var profService = new ProfessionalService
        {
            ProfessionalId = professionalId,
            BranchServiceId = branchServiceId
        };
        await _unitOfWork.Repository<ProfessionalService>().AddAsync(profService);
        await _unitOfWork.SaveChangesAsync();
    }
}
