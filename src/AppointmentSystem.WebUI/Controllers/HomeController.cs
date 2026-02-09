using AppointmentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentSystem.WebUI.Controllers;

public class HomeController : Controller
{
    private readonly IBranchManagementService _branchService;

    public HomeController(IBranchManagementService branchService)
    {
        _branchService = branchService;
    }

    public async Task<IActionResult> Index()
    {
        var branches = await _branchService.GetAllBranchesAsync();
        return View(branches);
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
