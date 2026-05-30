using AppointmentSystem.Application.Interfaces;
using AppointmentSystem.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Markdig;
using System.Text.RegularExpressions;

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

        var raw = System.IO.File.ReadAllText(markdownPath, System.Text.Encoding.UTF8);

        // Parsear front-matter YAML (---...---)
        var hero = new HeroViewModel { Title = "Nosotros", Height = "380px" };
        string markdownBody = raw;

        var fmMatch = Regex.Match(raw, @"^---\s*\n(.*?)\n---\s*\n", RegexOptions.Singleline);
        if (fmMatch.Success)
        {
            var fm = fmMatch.Groups[1].Value;
            hero.Title    = GetFrontMatterValue(fm, "hero_title") ?? hero.Title;
            hero.Subtitle = GetFrontMatterValue(fm, "hero_subtitle") ?? string.Empty;
            hero.ImageUrl = GetFrontMatterValue(fm, "hero_image");
            var h = GetFrontMatterValue(fm, "hero_height");
            if (!string.IsNullOrEmpty(h)) hero.Height = h;
            markdownBody = raw[(fmMatch.Index + fmMatch.Length)..];
        }

        var html = Markdown.ToHtml(markdownBody);

        ViewBag.Hero = hero;
        ViewBag.ContentHtml = html;
        ViewBag.BusinessName = _appConfig.BusinessName;
        return View();
    }

    private static string? GetFrontMatterValue(string fm, string key)
    {
        var m = Regex.Match(fm, $@"^{Regex.Escape(key)}\s*:\s*(.+)$", RegexOptions.Multiline);
        return m.Success ? m.Groups[1].Value.Trim() : null;
    }

    public IActionResult Contact()
    {
        ViewBag.AppConfig = _appConfig;
        ViewBag.BusinessName = _appConfig.BusinessName;
        return View();
    }
}
