using System.ComponentModel.DataAnnotations;

namespace Mango.Client.Web.Models.Coupon.ViewModels;

public record CouponVm(
    int Id,
    [Required] 
    string Code,
    [
        Required,
        Range(0, 100)
    ]
    decimal DiscountAmount,
    [Range(0, int.MaxValue)] 
    decimal? MinimumAmount
) : CreateCouponVm(
    Code,
    DiscountAmount,
    MinimumAmount
);