using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Mango.Service.Coupon.Web.Models.Extensions;

public static class UseMigrationsExtension
{
    public static async Task<IHost> UseMigrations(this IHost entry, Assembly assembly)
    {
        IEnumerable<Type> contexts = assembly.GetTypes()
            .Where(type => type.IsSubclassOf(typeof(DbContext)))
            .ToArray();

        if (contexts.Any())
        {
            using (IServiceScope scope = entry.Services.CreateScope())
            {
                foreach (Type context in contexts)
                {
                    object? contextInstance = scope.ServiceProvider.GetService(context);

                    if (contextInstance != null)
                    {
                        if ((await ((DbContext)contextInstance).Database.GetPendingMigrationsAsync()).Any())
                        {
                            await ((DbContext)contextInstance).Database.MigrateAsync();
                        }
                    }
                }
            }
        }

        return entry;
    }
}