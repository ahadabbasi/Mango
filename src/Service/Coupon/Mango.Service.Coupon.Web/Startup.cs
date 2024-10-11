using System;
using Mango.Service.Coupon.Web.Models.Databases.Contexts;
using Mango.Service.Coupon.Web.Models.Extensions;
using Mango.Service.Coupon.Web.Models.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Mango.Service.Coupon.Web;

public static class Startup
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.
        services.Configure<MySqlConnectionBuilder>(
            configuration.GetSection(
                MySqlConnectionBuilder.ApplicationSettingSectionName
            )
        );

        services.AddDbContext<ApplicationContext>((provider, builder) =>
        {
            IOptions<MySqlConnectionBuilder>? connectionBuilder = provider.GetService<IOptions<MySqlConnectionBuilder>>();

            if (connectionBuilder == null)
            {
                throw new Exception("Connection builder is null");
            }

            builder.UseMySQL(connectionBuilder.Value.ConnectionString())
                .UseSnakeCaseNamingConvention();
        });

        services.AddAutoMapper(typeof(Startup).Assembly);

        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });
    }

    public static async void Configure(WebApplication app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        
        app.MapControllers();

        await app.UseMigrations(typeof(Startup).Assembly);

        
    }
}