using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Mango.Common.Shared.Configurations;
using Mango.Service.Coupon.Web.Models.Databases.Contexts;
using Mango.Service.Coupon.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mango.Service.Coupon.Web.Controllers;

[ApiController, Route(template: $"{Routes.PrefixApplicationRoute}/[controller]")]
public class CouponController(
    ApplicationContext context,
    IMapper mapper
) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Models.Entities.Coupon>>> Get(CancellationToken cancellationToken)
    {
        return await context.Coupons.ToListAsync(cancellationToken);
    }

    /*
    [HttpGet(template: "{code:regex(^\\w*$)}")]
    public async Task<ActionResult<Models.Entities.Coupon?>> Get(string code, CancellationToken cancellationToken)
    {
        return await context.Coupons
            .Where(model => model.Code.ToUpper().Equals(code.ToUpper()))
            .FirstOrDefaultAsync(cancellationToken);
    }
    */

    [HttpGet(template: "{id:int}")]
    public async Task<ActionResult<Models.Entities.Coupon?>> Get(int id, CancellationToken cancellationToken)
    {
        return await context.Coupons
            .Where(model => model.Id.Equals(id))
            .FirstOrDefaultAsync(cancellationToken);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateVm entry, CancellationToken cancellationToken)
    {
        IActionResult result = BadRequest(ModelState);

        if (ModelState.IsValid)
        {
            if (!await context.Coupons.AnyAsync(model => model.Code.ToUpper().Equals(entry.Code.ToUpper()),
                    cancellationToken))
            {
                Models.Entities.Coupon entity = mapper.Map<Models.Entities.Coupon>(entry);

                await context.Coupons.AddAsync(entity, cancellationToken);

                try
                {
                    await context.SaveChangesAsync(cancellationToken);

                    result = Ok();

                    ModelState.Clear();
                }
                catch (Exception)
                {
                    ModelState.AddModelError("UnknownException",
                        "An unexpected error has occurred; please try again later.");
                }
            }
            else
            {
                ModelState.AddModelError("DuplicateCouponCode",
                    "A coupon code already exists for this code; please select a different one.");
            }
        }

        return result;
    }

    [HttpPut(template: "{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateVm entry, CancellationToken cancellationToken)
    {
        IActionResult result = BadRequest(ModelState);

        if (ModelState.IsValid)
        {
            if (await context.Coupons.AnyAsync(model => model.Id == id, cancellationToken))
            {
                if (!await context.Coupons.AnyAsync(
                        model => model.Id != id && model.Code.ToUpper().Equals(entry.Code.ToUpper()),
                        cancellationToken)
                   )
                {
                    Models.Entities.Coupon entity = mapper.Map<Models.Entities.Coupon>(entry);

                    entity.Id = id;

                    context.Coupons.Update(entity);

                    try
                    {
                        await context.SaveChangesAsync(cancellationToken);

                        result = Ok();

                        ModelState.Clear();
                    }

                    catch (Exception)
                    {
                        ModelState.AddModelError("UnknownException",
                            "An unexpected error has occurred; please try again later.");
                    }
                }
                else
                {
                    ModelState.AddModelError("DuplicateCouponCode",
                        "A coupon code already exists for this code; please select a different one.");
                }
            }

            else
            {
                ModelState.AddModelError("NotExistCoupon",
                    "No coupons are associated with this primary key. Please verify your information and try again.");
            }
        }

        return result;
    }

    [HttpDelete(template: "{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        IActionResult result = BadRequest(ModelState);

        if (await context.Coupons.AnyAsync(model => model.Id == id, cancellationToken))
        {
            Models.Entities.Coupon entity =
                await context.Coupons.FirstAsync(model => model.Id == id, cancellationToken);

            context.Coupons.Remove(entity);

            try
            {
                await context.SaveChangesAsync(cancellationToken);

                result = Ok();

                ModelState.Clear();
            }
            catch (Exception)
            {
                ModelState.AddModelError("UnknownException",
                    "An unexpected error has occurred; please try again later.");
            }
        }
        else
        {
            ModelState.AddModelError("NotExistCoupon",
                "No coupons are associated with this primary key. Please verify your information and try again.");
        }

        return result;
    }
}