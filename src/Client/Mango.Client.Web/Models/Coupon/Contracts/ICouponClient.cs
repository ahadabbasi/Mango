using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Mango.Client.Web.Models.Coupon.Contracts;

public interface ICouponClient
{
    Task<HttpClient> ClientAsync(CancellationToken cancellationToken = default);

    Task<JsonSerializerOptions> JsonSerializerOptionsAsync(CancellationToken cancellationToken = default);
}