using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Mango.Client.Web.Models.Configurations;
using Mango.Client.Web.Models.Coupon.Contracts;
using Mango.Client.Web.Models.Coupon.ViewModels;
using Mango.Common.Shared.Result;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Client.Web.Areas.Admin.Controllers;

[Area(AreaName.Admin)]
public class CouponController(ICouponService service) : Controller
{
    // GET
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        return View(await service.AllAsync(cancellationToken));
    }
}