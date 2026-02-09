using AppointmentSystem.Application.DTOs;
using AppointmentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentSystem.WebUI.Controllers;

public class BookingController : Controller
{
    private readonly IBranchManagementService _branchService;
    private readonly IAppointmentService _appointmentService;

    public BookingController(IBranchManagementService branchService, IAppointmentService appointmentService)
    {
        _branchService = branchService;
        _appointmentService = appointmentService;
    }

    public async Task<IActionResult> Services(Guid branchId)
    {
        var branch = await _branchService.GetBranchByIdAsync(branchId);
        if (branch == null) return NotFound();

        ViewBag.BranchId = branchId;
        ViewBag.BranchName = branch.Name;
        var services = await _branchService.GetServicesByBranchAsync(branchId);
        return View(services);
    }

    public async Task<IActionResult> Professionals(Guid branchId, Guid branchServiceId)
    {
        var professionals = await _branchService.GetProfessionalsByServiceAsync(branchServiceId);
        ViewBag.BranchId = branchId;
        ViewBag.BranchServiceId = branchServiceId;
        return View(professionals);
    }

    public async Task<IActionResult> Slots(Guid branchId, Guid branchServiceId, Guid professionalId, DateTime? date)
    {
        var selectedDate = date ?? DateTime.Today;
        if (selectedDate < DateTime.Today) selectedDate = DateTime.Today;

        var slots = await _branchService.GetAvailableSlotsAsync(branchServiceId, professionalId, selectedDate);

        ViewBag.BranchId = branchId;
        ViewBag.BranchServiceId = branchServiceId;
        ViewBag.ProfessionalId = professionalId;
        ViewBag.SelectedDate = selectedDate;

        return View(slots);
    }

    [HttpGet]
    public IActionResult Create(Guid branchId, Guid branchServiceId, Guid professionalId, DateTime startTime)
    {
        var dto = new CreateAppointmentDto
        {
            BranchId = branchId,
            BranchServiceId = branchServiceId,
            ProfessionalId = professionalId,
            StartTime = startTime
        };
        return View(dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateAppointmentDto dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var id = await _appointmentService.CreateAppointmentAsync(dto);
                return RedirectToAction("Confirmation", new { id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
        }
        return View(dto);
    }

    public async Task<IActionResult> Confirmation(Guid id)
    {
        // For simplicity, just show a success message.
        // In a real app, fetch appointment details.
        return View(id);
    }
}
