using AppointmentSystem.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace AppointmentSystem.WebUI.Filters;

public class InjectAppConfigFilter : IActionFilter
{
    private readonly AppConfig _appConfig;

    public InjectAppConfigFilter(IOptions<AppConfig> appConfig)
    {
        _appConfig = appConfig.Value;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.Controller is Controller controller)
        {
            controller.ViewBag.BusinessName = _appConfig.BusinessName;
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
