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

[Area(AreaName.Admin), Route("[area]/[controller]")]
public sealed class CouponController(ICouponService service) : Controller
{
    [HttpGet("[action]", Name = Routes.DashboardCouponList)]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
    {
        return View(await service.AllAsync(cancellationToken));
    }
}