using AppointmentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentSystem.WebUI.Controllers;

public class AppointmentsController : Controller
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentsController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> List(string email)
    {
        if (string.IsNullOrEmpty(email)) return RedirectToAction("Index");
        var appointments = await _appointmentService.GetClientAppointmentsAsync(email);
        ViewBag.Email = email;
        return View(appointments);
    }

    [HttpPost]
    public async Task<IActionResult> Cancel(Guid id, string email)
    {
        try
        {
            await _appointmentService.CancelAppointmentAsync(id);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }
        return RedirectToAction("List", new { email });
    }
}
