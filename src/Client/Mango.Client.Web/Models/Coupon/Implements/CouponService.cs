using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Mango.Client.Web.Models.Coupon.Contracts;
using Mango.Client.Web.Models.Coupon.ViewModels;
using Mango.Common.Shared.Result;

namespace Mango.Client.Web.Models.Coupon.Implements;

public sealed class CouponService(ICouponClient client) : ICouponService
{
    public async Task<Result<IEnumerable<CouponVm>>> AllAsync(CancellationToken cancellationToken = default)
    {
        Result<IEnumerable<CouponVm>> result = Error.None;

        try
        {
            using (HttpClient httpClient = await client.ClientAsync(cancellationToken))
            {
                using (HttpRequestMessage request = new HttpRequestMessage())
                {
                    request.Method = HttpMethod.Get;

                    using (HttpResponseMessage response = await httpClient.SendAsync(request, cancellationToken))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            IEnumerable<CouponVm>? enumerable =
                                await System.Text.Json.JsonSerializer.DeserializeAsync<IEnumerable<CouponVm>>(
                                    await response.Content.ReadAsStreamAsync(cancellationToken),
                                    await client.JsonSerializerOptionsAsync(cancellationToken),
                                    cancellationToken
                                );

                            if (enumerable != null)
                            {
                                result = Result<IEnumerable<CouponVm>>.Success(enumerable);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            result = Error.ServerNotResponse;
        }

        return result;
    }

    public Task<Result<CouponVm>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new System.NotImplementedException();
    }

    public Task<Result<CouponVm>> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        throw new System.NotImplementedException();
    }

    public Task<Result> CreateAsync(CreateCouponVm entry, CancellationToken cancellationToken = default)
    {
        throw new System.NotImplementedException();
    }

    public Task<Result> UpdateAsync(int id, CreateCouponVm entry, CancellationToken cancellationToken = default)
    {
        throw new System.NotImplementedException();
    }

    public Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new System.NotImplementedException();
    }
}