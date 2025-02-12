using System.Net.Http;
using Mango.Client.Web.Models.Commons.Handlers;
using Mango.Client.Web.Models.Coupon.Contracts;
using Mango.Client.Web.Models.Coupon.Implements;
using Mango.Client.Web.Models.Coupon.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Mango.Client.Web;

public static class Startup
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.

        services.AddControllersWithViews();

        services.AddHttpClient();

        services.Configure<CouponConfiguration>(
            configuration.GetSection(CouponConfiguration.ApplicationSettingSectionName)
        );
        
        
        services.AddScoped<EnvironmentHttpClientHandler>();

        services.AddHttpClient<ICouponClient, CouponClient>()
            .ConfigurePrimaryHttpMessageHandler<EnvironmentHttpClientHandler>();

        services.AddScoped<ICouponService, CouponService>();

        services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });
    }

    public static void Configure(WebApplication app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.

        if (!env.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
        );

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    }
}