using AppointmentSystem.Application.Interfaces;
using AppointmentSystem.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Markdig;

namespace AppointmentSystem.WebUI.Controllers;

public class HomeController : Controller
{
    private readonly IBranchManagementService _branchService;
    private readonly AppConfig _appConfig;
    private readonly IWebHostEnvironment _env;

    public HomeController(
        IBranchManagementService branchService,
        IOptions<AppConfig> appConfig,
        IWebHostEnvironment env)
    {
        _branchService = branchService;
        _appConfig = appConfig.Value;
        _env = env;
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

    public IActionResult About()
    {
        var markdownPath = Path.Combine(_env.ContentRootPath, "Content", "about.md");
        
        if (!System.IO.File.Exists(markdownPath))
        {
            return NotFound();
        }

        var markdown = System.IO.File.ReadAllText(markdownPath);
        var html = Markdown.ToHtml(markdown);
        
        ViewBag.ContentHtml = html;
        return View();
    }

    public IActionResult Contact()
    {
        ViewBag.AppConfig = _appConfig;
        return View();
    }
}
