using AppointmentSystem.Application.DTOs;
using AppointmentSystem.Application.Interfaces;
using AppointmentSystem.Domain.Entities;
using AppointmentSystem.Domain.Interfaces;

namespace AppointmentSystem.Application.Services;

public class BranchManagementService : IBranchManagementService
{
    private readonly IUnitOfWork _unitOfWork;

    public BranchManagementService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<BranchDto>> GetAllBranchesAsync()
    {
        var branches = await _unitOfWork.Repository<Branch>().GetAllAsync();
        return branches.Select(b => new BranchDto
        {
            Id = b.Id,
            Name = b.Name,
            Address = b.Address,
            Phone = b.Phone,
            Email = b.Email
        });
    }

    public async Task<BranchDto?> GetBranchByIdAsync(Guid id)
    {
        var b = await _unitOfWork.Repository<Branch>().GetByIdAsync(id);
        if (b == null) return null;
        return new BranchDto
        {
            Id = b.Id,
            Name = b.Name,
            Address = b.Address,
            Phone = b.Phone,
            Email = b.Email
        };
    }

    public async Task<IEnumerable<ServiceDto>> GetServicesByBranchAsync(Guid branchId)
    {
        var branchServices = await _unitOfWork.Repository<BranchService>()
            .FindAsync(bs => bs.BranchId == branchId && bs.IsActive, bs => bs.Service);

        return branchServices.Select(bs => new ServiceDto
        {
            Id = bs.Id,
            Name = bs.Service.Name,
            Description = bs.Service.Description,
            Category = bs.Service.Category.ToString(),
            DurationMinutes = bs.DurationMinutes,
            Price = bs.Price
        });
    }

    public async Task<IEnumerable<ProfessionalDto>> GetProfessionalsByServiceAsync(Guid branchServiceId)
    {
        var profServices = await _unitOfWork.Repository<ProfessionalService>()
            .FindAsync(ps => ps.BranchServiceId == branchServiceId, ps => ps.Professional);

        return profServices.Where(ps => ps.Professional.IsActive).Select(ps => new ProfessionalDto
        {
            Id = ps.Professional.Id,
            FullName = $"{ps.Professional.FirstName} {ps.Professional.LastName}",
            Email = ps.Professional.Email
        });
    }

    public async Task<IEnumerable<DateTime>> GetAvailableSlotsAsync(Guid branchServiceId, Guid professionalId, DateTime date)
    {
        var bs = await _unitOfWork.Repository<BranchService>().GetByIdAsync(branchServiceId);
        if (bs == null) return Enumerable.Empty<DateTime>();

        var duration = bs.DurationMinutes;
        var start = date.Date.AddHours(9);
        var end = date.Date.AddHours(17);

        // Fetch existing appointments for the professional on that day
        var existingAppointments = await _unitOfWork.Repository<Appointment>()
            .FindAsync(a => a.ProfessionalId == professionalId && a.StartTime.Date == date.Date && a.Status != Domain.Enums.AppointmentStatus.Cancelled);

        // Fetch all branch appointments to check capacity
        var branch = await _unitOfWork.Repository<Branch>().GetByIdAsync(bs.BranchId);
        var branchAppointments = await _unitOfWork.Repository<Appointment>()
            .FindAsync(a => a.BranchId == bs.BranchId && a.StartTime.Date == date.Date && a.Status != Domain.Enums.AppointmentStatus.Cancelled);

        var slots = new List<DateTime>();
        var current = start;
        while (current.AddMinutes(duration) <= end)
        {
            var slotEnd = current.AddMinutes(duration);

            bool isProfessionalOccupied = existingAppointments.Any(a =>
                current < a.EndTime && slotEnd > a.StartTime);

            bool isBranchFull = branch != null && branchAppointments.Count(a =>
                current < a.EndTime && slotEnd > a.StartTime) >= branch.SimultaneousCapacity;

            if (!isProfessionalOccupied && !isBranchFull)
            {
                slots.Add(current);
            }
            current = current.AddMinutes(30);
        }

        return slots;
    }
}
