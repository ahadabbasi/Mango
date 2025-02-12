using System;
using System.Threading;
using System.Threading.Tasks;
using Mango.Client.Web.Areas.Admin.Models.Configurations;
using Mango.Client.Web.Models.Configurations;
using Mango.Client.Web.Models.Coupon.Contracts;
using Mango.Client.Web.Models.Coupon.ViewModels;
using Mango.Common.Shared.Result;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Client.Web.Areas.Admin.Controllers;

[Area(AreaName.Admin), Route(template: "[area]/[controller]")]
public sealed class CouponController(ICouponService service) : Controller
{
    [HttpGet(template: "[action]", Name = Routes.DashboardCouponList)]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
    {
        return View(await service.AllAsync(cancellationToken));
    }

    [HttpGet(template: "[action]", Name = Routes.DashboardCouponCreate)]
    public Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        return Task.FromResult<IActionResult>(View());
    }
    
    [
        HttpPost(template: "[action]", Name = Routes.DashboardCouponCreate),
        ValidateAntiForgeryToken
    ]
    public async Task<IActionResult> Create([Bind] CreateCouponVm entry, CancellationToken cancellationToken)
    {
        IActionResult result = View(entry);
        
        if (ModelState.IsValid)
        {
            Result resultOfService = await service.CreateAsync(entry, cancellationToken);

            if (resultOfService.IsSuccess)
            {
                result = RedirectToRoute(Routes.DashboardCouponList);
            }
            
            ModelState.Clear();

            foreach (Error error in resultOfService.Errors ?? Array.Empty<Error>())
            {
                ModelState.TryAddModelError(error.Code, error.Description ?? string.Empty);
            }
        }

        return result;
    }
}