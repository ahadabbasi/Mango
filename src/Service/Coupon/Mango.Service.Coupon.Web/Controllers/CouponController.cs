using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mango.Service.Coupon.Web.Models.Databases.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mango.Service.Coupon.Web.Controllers;

[ApiController, Route("api/[controller]")]
public class CouponController(ApplicationContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Models.Entities.Coupon>>> Get(CancellationToken cancellationToken)
    {
        return await context.Coupons.ToListAsync(cancellationToken);
    }
    
    [HttpGet("{code:regex(^\\w*$)}")]
    public async Task<ActionResult<Models.Entities.Coupon?>> Get(string code, CancellationToken cancellationToken)
    {
        return await context.Coupons
            .Where(model => model.Code.ToUpper().Equals(code.ToUpper()))
            .FirstOrDefaultAsync(cancellationToken);
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Models.Entities.Coupon?>> Get(int id, CancellationToken cancellationToken)
    {
        return await context.Coupons
            .Where(model => model.Id.Equals(id))
            .FirstOrDefaultAsync(cancellationToken);
    }
}