namespace Mango.Service.Coupon.Web.Models.Entities;

/// <summary>
/// 
/// </summary>
public sealed class Coupon
{
    /// <summary>
    /// 
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string Code { get; set; } = string.Empty;
    
    /// <summary>
    /// 
    /// </summary>
    public decimal DiscountAmount { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public decimal? MinimumAmount { get; set; }
}