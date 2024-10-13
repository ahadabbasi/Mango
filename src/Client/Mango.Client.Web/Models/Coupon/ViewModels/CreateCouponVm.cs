namespace Mango.Client.Web.Models.Coupon.ViewModels;

public record CreateCouponVm(string Code, decimal DiscountAmount, decimal? MinimumAmount);