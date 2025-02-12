using System.ComponentModel.DataAnnotations;

namespace Mango.Service.Coupon.Web.Models.ViewModels;

public record CreateVm(
    [Required]
    string Code, 
    [
        Required,
        Range(0, 100)
    ]
    decimal DiscountAmount,
    [
        Range(0, int.MaxValue)
    ]
    decimal? MinimumAmount
);