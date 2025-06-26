using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Net.Http;

namespace Mango.Client.Web.Models.Commons.Handlers;

public sealed class EnvironmentHttpClientHandler : HttpClientHandler
{
    public EnvironmentHttpClientHandler(IWebHostEnvironment environment)
    {
        if(environment.IsDevelopment())
        {
            ServerCertificateCustomValidationCallback = DangerousAcceptAnyServerCertificateValidator;
        }
    }
}