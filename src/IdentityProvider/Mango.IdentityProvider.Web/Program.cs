using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace Mango.IdentityProvider.Web;

public static class Program
{
    public static Task Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        Startup.ConfigureServices(builder.Services, builder.Configuration);

        WebApplication app = builder.Build();

        Startup.Configure(app, app.Environment);

        return app.RunAsync();
    }
}