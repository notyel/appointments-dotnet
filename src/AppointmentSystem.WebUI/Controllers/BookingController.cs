using AppointmentSystem.Application.DTOs;
using AppointmentSystem.Application.Helpers;
using AppointmentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentSystem.WebUI.Controllers;

public class BookingController : Controller
{
    private readonly IBranchManagementService _branchService;
    private readonly IAppointmentService _appointmentService;
    private readonly IFormatSettings _format;

    public BookingController(IBranchManagementService branchService, IAppointmentService appointmentService, IFormatSettings format)
    {
        _branchService = branchService;
        _appointmentService = appointmentService;
        _format = format;
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

        var branch = await _branchService.GetBranchByIdAsync(branchId);
        var services = await _branchService.GetServicesByBranchAsync(branchId);
        var selectedService = services.FirstOrDefault(s => s.Id == branchServiceId);

        ViewBag.BranchName = branch?.Name;
        ViewBag.BranchImageUrl = branch?.ImageUrl;
        ViewBag.ServiceName = selectedService?.Name;
        ViewBag.ServicePrice = selectedService?.Price;
        ViewBag.ServiceFormattedPrice = selectedService?.Price is decimal p1 ? FormatHelper.FormatCurrency(p1, _format) : null;
        ViewBag.ServiceDuration = selectedService?.DurationMinutes;
        ViewBag.ServiceImageUrl = selectedService?.ImageUrl;
        ViewBag.FormattedDate = null;

        return View(professionals);
    }

    public async Task<IActionResult> Slots(Guid branchId, Guid branchServiceId, Guid professionalId, DateTime? date)
    {
        var today = DateTime.Today;
        var maxDate = today.AddDays(_format.MaxBookingDays);

        var selectedDate = date?.Date ?? today;
        if (selectedDate < today)   selectedDate = today;
        if (selectedDate > maxDate) selectedDate = maxDate;

        var slots = await _branchService.GetAvailableSlotsAsync(branchServiceId, professionalId, selectedDate);

        var branch = await _branchService.GetBranchByIdAsync(branchId);
        var services = await _branchService.GetServicesByBranchAsync(branchId);
        var selectedService = services.FirstOrDefault(s => s.Id == branchServiceId);
        var professionals = await _branchService.GetProfessionalsByServiceAsync(branchServiceId);
        var selectedProfessional = professionals.FirstOrDefault(p => p.Id == professionalId);

        // Días para el carrusel: desde hoy hasta maxDate
        var calendarDays = Enumerable.Range(0, _format.MaxBookingDays + 1)
            .Select(i => today.AddDays(i))
            .ToList();

        // Semana visible: la semana que contiene la fecha seleccionada (lunes)
        var weekStart = selectedDate.AddDays(-(int)selectedDate.DayOfWeek + (selectedDate.DayOfWeek == DayOfWeek.Sunday ? -6 : 1));

        ViewBag.BranchId = branchId;
        ViewBag.BranchServiceId = branchServiceId;
        ViewBag.ProfessionalId = professionalId;
        ViewBag.SelectedDate = selectedDate;
        ViewBag.MaxDate = maxDate;
        ViewBag.WeekStart = weekStart;
        ViewBag.CalendarDays = calendarDays;

        ViewBag.BranchName = branch?.Name;
        ViewBag.BranchImageUrl = branch?.ImageUrl;
        ViewBag.ServiceName = selectedService?.Name;
        ViewBag.ServicePrice = selectedService?.Price;
        ViewBag.ServiceFormattedPrice = selectedService?.Price is decimal p2 ? FormatHelper.FormatCurrency(p2, _format) : null;
        ViewBag.ServiceDuration = selectedService?.DurationMinutes;
        ViewBag.ServiceImageUrl = selectedService?.ImageUrl;
        ViewBag.ProfessionalName = selectedProfessional?.FullName;
        ViewBag.ProfessionalImageUrl = selectedProfessional?.ImageUrl;
        ViewBag.FormattedDate = FormatHelper.FormatDateTime(selectedDate.ToUniversalTime(), _format, "dddd, d 'de' MMMM 'de' yyyy");

        return View(slots);
    }

    [HttpGet]
    public async Task<IActionResult> Create(Guid branchId, Guid branchServiceId, Guid professionalId, DateTime startTime)
    {
        var dto = new CreateAppointmentDto
        {
            BranchId = branchId,
            BranchServiceId = branchServiceId,
            ProfessionalId = professionalId,
            StartTime = startTime
        };

        var branch = await _branchService.GetBranchByIdAsync(branchId);
        var services = await _branchService.GetServicesByBranchAsync(branchId);
        var selectedService = services.FirstOrDefault(s => s.Id == branchServiceId);
        var professionals = await _branchService.GetProfessionalsByServiceAsync(branchServiceId);
        var selectedProfessional = professionals.FirstOrDefault(p => p.Id == professionalId);

        ViewBag.BranchName = branch?.Name;
        ViewBag.BranchImageUrl = branch?.ImageUrl;
        ViewBag.ServiceName = selectedService?.Name;
        ViewBag.ServicePrice = selectedService?.Price;
        ViewBag.ServiceFormattedPrice = selectedService?.Price is decimal p3 ? FormatHelper.FormatCurrency(p3, _format) : null;
        ViewBag.ServiceDuration = selectedService?.DurationMinutes;
        ViewBag.ServiceImageUrl = selectedService?.ImageUrl;
        ViewBag.ProfessionalName = selectedProfessional?.FullName;
        ViewBag.ProfessionalImageUrl = selectedProfessional?.ImageUrl;
        ViewBag.SelectedDate = startTime;
        ViewBag.FormattedDate = FormatHelper.FormatDateTime(startTime.ToUniversalTime(), _format, "dddd, d 'de' MMMM 'de' yyyy");
        ViewBag.FormattedTime = FormatHelper.FormatDateTime(startTime.ToUniversalTime(), _format, "hh:mm tt");

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
