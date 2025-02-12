namespace Mango.Client.Web.Models.Coupon.ViewModels;

public record CouponVm(
    int Id, 
    string Code,
    decimal DiscountAmount, 
    decimal? MinimumAmount
) : CreateCouponVm(
    Code, 
    DiscountAmount, 
    MinimumAmount
);