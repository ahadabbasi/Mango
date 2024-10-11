namespace Mango.Service.Coupon.Web.Models.ViewModels;

public record CreateVm(string Code, decimal DiscountAmount, decimal? MinimumAmount);