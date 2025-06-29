﻿using Mango.Client.Web.Areas.Admin.Models.Configurations;
using Mango.Client.Web.Models.Configurations;
using Mango.Client.Web.Models.Coupon.Contracts;
using Mango.Client.Web.Models.Coupon.ViewModels;
using Mango.Common.Shared.Result;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mango.Client.Web.Areas.Admin.Controllers;

[Area(AreaName.Admin), Route(template: "[area]/[controller]")]
public sealed class CouponController(ICouponService service) : Controller
{
    [HttpGet(template: "[action]", Name = IRoutes.DashboardCouponList)]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
    {
        return View(await service.AllAsync(cancellationToken));
    }

    [HttpGet(template: "[action]", Name = IRoutes.DashboardCouponCreate)]
    public Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        return Task.FromResult<IActionResult>(View());
    }
    
    [
        HttpPost(template: "[action]", Name = IRoutes.DashboardCouponCreate),
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
                result = RedirectToRoute(IRoutes.DashboardCouponList);
            }
            
            ModelState.Clear();

            foreach (Error error in resultOfService.Errors ?? Array.Empty<Error>())
            {
                ModelState.TryAddModelError(error.Code, error.Description ?? string.Empty);
            }
        }

        return result;
    }
    
    [HttpGet(template: "[action]/{id:int}", Name = IRoutes.DashboardCouponUpdate)]
    public async Task<IActionResult> Update([FromRoute]int? id, CancellationToken cancellationToken)
    {
        IActionResult result = RedirectToRoute(IRoutes.DashboardCouponList);

        if (id != null)
        {
            Result<CouponVm> resultOfService = await service.GetByIdAsync((int)id, cancellationToken);

            if (resultOfService.IsSuccess)
            {
                result = View(resultOfService.Value);
            }
        }
        
        return result;
    }
    
    [
        HttpPost(template: "[action]/{id:int}", Name = IRoutes.DashboardCouponUpdate),
        ValidateAntiForgeryToken
    ]
    public async Task<IActionResult> Update([FromRoute]int id, [Bind]CouponVm entry, CancellationToken cancellationToken)
    {
        IActionResult result = View(entry);

        if (ModelState.IsValid)
        {
            Result resultOfService = await service.UpdateAsync(id, entry, cancellationToken);

            if (resultOfService.IsSuccess)
            {
                result = RedirectToRoute(IRoutes.DashboardCouponList);
            }
            
            ModelState.Clear();

            foreach (Error error in resultOfService.Errors ?? Array.Empty<Error>())
            {
                ModelState.TryAddModelError(error.Code, error.Description ?? string.Empty);
            }
        }
        
        return result;
    }
    
    [HttpGet(template: "[action]/{id:int}", Name = IRoutes.DashboardCouponDelete)]
    public async Task<IActionResult> Delete([FromRoute]int? id, CancellationToken cancellationToken)
    {
        IActionResult result = RedirectToRoute(IRoutes.DashboardCouponList);

        if (id != null)
        {
            Result<CouponVm> resultOfService = await service.GetByIdAsync((int)id, cancellationToken);

            if (resultOfService.IsSuccess)
            {
                result = View(resultOfService.Value);
            }
        }
        
        return result;
    }
    
    [
        HttpPost(template: "[action]/{id:int}", Name = IRoutes.DashboardCouponDelete),
        ValidateAntiForgeryToken
    ]
    public async Task<IActionResult> Delete([FromRoute]int id, [Bind]CouponVm entry, CancellationToken cancellationToken)
    {
        IActionResult result = View(entry);

        if (ModelState.IsValid)
        {
            Result resultOfService = await service.DeleteAsync(id, cancellationToken);

            if (resultOfService.IsSuccess)
            {
                result = RedirectToRoute(IRoutes.DashboardCouponList);
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