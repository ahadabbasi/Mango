using AutoMapper;
using Mango.Service.Coupon.Web.Models.ViewModels;

namespace Mango.Service.Coupon.Web.Models.Profiles;

public class CreateVmToCouponProfile : Profile
{
    public CreateVmToCouponProfile()
    {
        CreateMap<CreateVm, Entities.Coupon>();
    }
}