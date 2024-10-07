using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mango.Service.Coupon.Web.Models.Databases.Configurations;

public sealed class CouponConfiguration : IEntityTypeConfiguration<Entities.Coupon>
{
    public void Configure(EntityTypeBuilder<Entities.Coupon> builder)
    {
        builder.HasIndex(model => model.Id);

        builder.Property(model => model.Code)
            .IsRequired();

        builder.Property(model => model.DiscountAmount)
            .IsRequired();
    }
}