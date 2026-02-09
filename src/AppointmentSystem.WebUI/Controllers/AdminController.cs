using AppointmentSystem.Application.DTOs;
using AppointmentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentSystem.WebUI.Controllers;

[Authorize(Roles = "Admin,BranchAdmin,Receptionist")]
public class AdminController : Controller
{
    private readonly IAdminService _adminService;
    private readonly IBranchManagementService _branchService;
    private readonly IAppointmentService _appointmentService;

    public AdminController(IAdminService adminService, IBranchManagementService branchService, IAppointmentService appointmentService)
    {
        _adminService = adminService;
        _branchService = branchService;
        _appointmentService = appointmentService;
    }

    public async Task<IActionResult> Index()
    {
        if (User.IsInRole("Admin"))
        {
            var branches = await _branchService.GetAllBranchesAsync();
            return View(branches);
        }
        else if (User.IsInRole("BranchAdmin"))
        {
            var branchId = User.FindFirst("BranchId")?.Value;
            if (Guid.TryParse(branchId, out var id))
            {
                var branch = await _branchService.GetBranchByIdAsync(id);
                return View(new List<BranchDto> { branch! });
            }
        }

        return View(new List<BranchDto>());
    }

    public async Task<IActionResult> Dashboard(Guid branchId)
    {
        // Simple Branch Isolation Check
        if (!User.IsInRole("Admin"))
        {
            // Simulate check for BranchAdmin role and specific branch
            var branchClaim = User.FindFirst("BranchId")?.Value;
            if (User.IsInRole("BranchAdmin") && branchClaim != branchId.ToString())
            {
                return Forbid();
            }
        }

        var branch = await _branchService.GetBranchByIdAsync(branchId);
        if (branch == null) return NotFound();

        var appointments = await _appointmentService.GetBranchAppointmentsAsync(branchId, DateTime.Today);
        ViewBag.Branch = branch;
        return View(appointments);
    }

    // Additional admin actions for managing branches, services, etc. would go here
}
