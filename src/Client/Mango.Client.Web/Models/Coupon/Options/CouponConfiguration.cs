namespace Mango.Client.Web.Models.Coupon.Options;

public sealed class CouponConfiguration
{
    public const string ApplicationSettingSectionName = "Coupon";
    
    public string Address { get; set; } = string.Empty;
}