using Mango.IdentityProvider.Web.Areas.Account.Models.Configurations;
using Mango.IdentityProvider.Web.Models.Configurations;
using Microsoft.AspNetCore.Mvc;

namespace Mango.IdentityProvider.Web.Areas.Account.Controllers;

[
    Area(areaName: AreaName.Account),
    Route(template: "[area]/[controller]")
]
public class LoginController : Controller
{
    [HttpGet(Name = Routes.AccountLogin)]
    public IActionResult Index()
    {
        return View();
    }
}