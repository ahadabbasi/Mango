using Mango.Common.Shared.Result;

namespace Mango.Service.Coupon.Web.Models;

public sealed class Errors
{
    public static readonly Error NotExist =
        new(
            "NotExistCoupon",
            "No coupons are associated with this primary key. Please verify your information and try again."
        );

    public static readonly Error DuplicateCode =
        new(
            "DuplicateCouponCode",
            "A coupon code already exists for this code; please select a different one."
        );
}