using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Mango.Client.Web.Models.Coupon.Contracts;
using Mango.Client.Web.Models.Coupon.Options;
using Microsoft.Extensions.Options;

namespace Mango.Client.Web.Models.Coupon.Implements;

public sealed class CouponClient(HttpClient client, IOptions<CouponConfiguration> options) : ICouponClient
{
    public Task<HttpClient> ClientAsync(CancellationToken cancellationToken = default)
    {
        client.BaseAddress = new Uri(options.Value.Address);
        
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(System.Net.Mime.MediaTypeNames.Application.Json));
        
        return Task.FromResult(client);
    }

    public Task<JsonSerializerOptions> JsonSerializerOptionsAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
    }
}