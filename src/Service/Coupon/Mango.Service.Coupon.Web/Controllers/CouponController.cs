using AutoMapper;
using Mango.Common.Shared.Configurations;
using Mango.Common.Shared.Extensions;
using Mango.Common.Shared.Result;
using Mango.Service.Coupon.Web.Models;
using Mango.Service.Coupon.Web.Models.Databases.Contexts;
using Mango.Service.Coupon.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mango.Service.Coupon.Web.Controllers;

[
    ApiController,
    Route(template: $"{Routes.PrefixApplicationRoute}/[controller]")
]
public sealed class CouponController(
    ApplicationContext context,
    IMapper mapper
) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Models.Entities.Coupon>>> Get(
        CancellationToken cancellationToken
    )
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
    public async Task<ActionResult<Models.Entities.Coupon?>> Get(
        [FromRoute] int id,
        CancellationToken cancellationToken
    )
    {
        return await context.Coupons
            .Where(model => model.Id.Equals(id))
            .FirstOrDefaultAsync(cancellationToken);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateVm entry, 
        CancellationToken cancellationToken
    )
    {
        IActionResult result = BadRequest(ModelState);

        if (ModelState.IsValid)
        {
            if (
                !await context.Coupons.AnyAsync(
                    model => model.Code.ToUpper().Equals(entry.Code.ToUpper()),
                    cancellationToken
                )
            )
            {
                var entity = mapper.Map<Models.Entities.Coupon>(entry);

                await context.Coupons.AddAsync(entity, cancellationToken);

                try
                {
                    await context.SaveChangesAsync(cancellationToken);

                    result = Ok();

                    ModelState.Clear();
                }
                catch (Exception)
                {
                    ModelState.AddModelError(Error.Unexpected);
                }
            }
            else
            {
                ModelState.AddModelError(Errors.DuplicateCode);
            }
        }

        return result;
    }

    [HttpPut(template: "{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id, 
        [FromBody] CreateVm entry, 
        CancellationToken cancellationToken
    )
    {
        IActionResult result = BadRequest(ModelState);


        if (ModelState.IsValid)
        {
            if (
                await context.Coupons.AnyAsync(
                    model => model.Id == id, 
                    cancellationToken
                )
            )
            {
                if (
                    !await context.Coupons.AnyAsync(
                        model => model.Id != id && model.Code.ToUpper().Equals(entry.Code.ToUpper()),
                        cancellationToken)
                   )
                {
                    var entity = mapper.Map<Models.Entities.Coupon>(entry);

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
                        ModelState.AddModelError(Error.Unexpected);
                    }
                }
                else
                {
                    ModelState.AddModelError(Errors.DuplicateCode);
                }
            }
            else
            {
                ModelState.AddModelError(Errors.NotExist);
            }
        }

        return result;
    }

    [HttpDelete(template: "{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        IActionResult result = BadRequest(ModelState);

        if (
            await context.Coupons.AnyAsync(
                model => model.Id == id,
                cancellationToken
            )
        )
        {
            var entity =
                await context.Coupons.FirstAsync(
                    model => model.Id == id,
                    cancellationToken
                );

            context.Coupons.Remove(entity);

            try
            {
                await context.SaveChangesAsync(cancellationToken);

                result = Ok();

                ModelState.Clear();
            }
            catch (Exception)
            {
                ModelState.AddModelError(Error.Unexpected);
            }
        }
        else
        {
            ModelState.AddModelError(Errors.NotExist);
        }

        return result;
    }
}