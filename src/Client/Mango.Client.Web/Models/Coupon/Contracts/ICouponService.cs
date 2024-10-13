using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Mango.Client.Web.Models.Coupon.ViewModels;
using Mango.Common.Shared.Result;

namespace Mango.Client.Web.Models.Coupon.Contracts;

public interface ICouponService
{
    Task<Result<IEnumerable<CouponVm>>> AllAsync(CancellationToken cancellationToken = default);

    Task<Result<CouponVm>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    
    Task<Result<CouponVm>> GetByCodeAsync(string code, CancellationToken cancellationToken = default);

    Task<Result> CreateAsync(CreateCouponVm entry, CancellationToken cancellationToken = default);
    
    Task<Result> UpdateAsync(int id, CreateCouponVm entry, CancellationToken cancellationToken = default);
    
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}