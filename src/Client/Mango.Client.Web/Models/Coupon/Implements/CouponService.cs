using Mango.Client.Web.Models.Commons.DataTransfers;
using Mango.Client.Web.Models.Coupon.Contracts;
using Mango.Client.Web.Models.Coupon.Options;
using Mango.Client.Web.Models.Coupon.ViewModels;
using Mango.Common.Shared.Result;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Mango.Client.Web.Models.Coupon.Implements;

public sealed class CouponService(
    ICouponClient client,
    IOptions<CouponConfiguration> options
) : ICouponService
{
    public async Task<Result<IEnumerable<CouponVm>>> AllAsync(CancellationToken cancellationToken = default)
    {
        Result<IEnumerable<CouponVm>> result = Error.None;

        try
        {
            using (HttpClient httpClient = await client.ClientAsync(cancellationToken))
            {
                using (HttpRequestMessage request = 
                    new(
                        HttpMethod.Get,
                        options.Value.EndPoint
                    )
                )
                {
                    using (HttpResponseMessage response = 
                        await httpClient.SendAsync(
                            request,
                            cancellationToken
                        )
                    )
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            IEnumerable<CouponVm>? entities =
                                await JsonSerializer.DeserializeAsync<IEnumerable<CouponVm>>(
                                    await response.Content.ReadAsStreamAsync(cancellationToken),
                                    await client.JsonSerializerOptionsAsync(cancellationToken),
                                    cancellationToken
                                );

                            if (entities != null)
                            {
                                result = Result<IEnumerable<CouponVm>>.Success(entities);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            result = Error.ServerNotResponse;
        }

        return result;
    }

    public async Task<Result<CouponVm>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        Result<CouponVm> result = Error.None;

        try
        {
            using (HttpClient httpClient = await client.ClientAsync(cancellationToken))
            {
                using (HttpRequestMessage request =
                    new(
                        HttpMethod.Get,
                        requestUri: $"{options.Value.EndPoint}/{id}"
                    )
                )
                {
                    using (HttpResponseMessage response = 
                        await httpClient.SendAsync(
                            request, 
                            cancellationToken
                        )
                    )
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            CouponVm? entity =
                                await JsonSerializer.DeserializeAsync<CouponVm>(
                                    await response.Content.ReadAsStreamAsync(cancellationToken),
                                    await client.JsonSerializerOptionsAsync(cancellationToken),
                                    cancellationToken
                                );

                            if (entity != null)
                            {
                                result = Result<CouponVm>.Success(entity);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            result = Error.ServerNotResponse;
        }

        return result;
    }

    public Task<Result<CouponVm>> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> CreateAsync(CreateCouponVm entry, CancellationToken cancellationToken = default)
    {
        Result result = Error.None;

        try
        {
            using (HttpClient httpClient = await client.ClientAsync(cancellationToken))
            {
                using (HttpRequestMessage request = 
                    new(
                        HttpMethod.Post,
                        options.Value.EndPoint
                    )
                )
                {
                    using (
                        StringContent content =
                        new(
                            JsonSerializer.Serialize(
                                entry, 
                                await client.JsonSerializerOptionsAsync(cancellationToken)
                            ),
                            Encoding.UTF8,
                            new MediaTypeHeaderValue(MediaTypeNames.Application.Json)
                        )
                    )
                    {
                        request.Content = content;

                        using (HttpResponseMessage response =
                            await httpClient.SendAsync(
                                request, 
                                cancellationToken
                            )
                        )
                        {
                            result = Result.Success();

                            if (!response.IsSuccessStatusCode)
                            {
                                result = Error.None;

                                BadRequestResponse? badRequestResponse =
                                    await JsonSerializer.DeserializeAsync<BadRequestResponse>(
                                        await response.Content.ReadAsStreamAsync(cancellationToken),
                                        await client.JsonSerializerOptionsAsync(cancellationToken),
                                        cancellationToken
                                    );

                                if (badRequestResponse != null)
                                {
                                    result = Result.Failure(
                                        badRequestResponse.Errors.SelectMany(
                                            item => item.Value.Select(
                                                (message, index) => new Error(Code: $"{item.Key}-{index}",
                                                    Description: message)
                                            )
                                        ).ToArray()
                                    );
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            result = Error.ServerNotResponse;
        }

        return result;
    }

    public async Task<Result> UpdateAsync(int id, CreateCouponVm entry, CancellationToken cancellationToken = default)
    {
        Result result = Error.None;

        try
        {
            using (HttpClient httpClient = await client.ClientAsync(cancellationToken))
            {
                using (HttpRequestMessage request =
                    new(
                        HttpMethod.Put,
                        requestUri: $"{options.Value.EndPoint}/{id}"
                    )
                )
                {
                    using (
                        StringContent content =
                        new(
                            JsonSerializer.Serialize(
                                entry, 
                                await client.JsonSerializerOptionsAsync(cancellationToken)
                            ),
                            Encoding.UTF8,
                            new MediaTypeHeaderValue(MediaTypeNames.Application.Json)
                        )
                    )
                    {
                        request.Content = content;

                        using (HttpResponseMessage response =
                            await httpClient.SendAsync(
                                request, 
                                cancellationToken
                            )
                        )
                        {
                            result = Result.Success();

                            if (!response.IsSuccessStatusCode)
                            {
                                result = Error.None;

                                BadRequestResponse? badRequestResponse =
                                    await JsonSerializer.DeserializeAsync<BadRequestResponse>(
                                        await response.Content.ReadAsStreamAsync(cancellationToken),
                                        await client.JsonSerializerOptionsAsync(cancellationToken),
                                        cancellationToken
                                    );

                                if (badRequestResponse != null)
                                {
                                    result = Result.Failure(
                                        badRequestResponse.Errors.SelectMany(
                                            item => item.Value.Select(
                                                (message, index) => new Error(Code: $"{item.Key}-{index}",
                                                    Description: message)
                                            )
                                        ).ToArray()
                                    );
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            result = Error.ServerNotResponse;
        }

        return result;
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        Result result = Error.None;

        try
        {
            using (HttpClient httpClient = await client.ClientAsync(cancellationToken))
            {
                using (HttpRequestMessage request =
                    new(HttpMethod.Delete, requestUri: $"{options.Value.EndPoint}/{id}")
                )
                {
                    using (HttpResponseMessage response = await httpClient.SendAsync(request, cancellationToken))
                    {
                        result = Result.Success();

                        if (!response.IsSuccessStatusCode)
                        {
                            result = Error.None;

                            BadRequestResponse? badRequestResponse =
                                await JsonSerializer.DeserializeAsync<BadRequestResponse>(
                                    await response.Content.ReadAsStreamAsync(cancellationToken),
                                    await client.JsonSerializerOptionsAsync(cancellationToken),
                                    cancellationToken
                                );

                            if (badRequestResponse != null)
                            {
                                result = Result.Failure(
                                    badRequestResponse.Errors.SelectMany(
                                        item => item.Value.Select(
                                            (message, index) =>
                                                new Error(Code: $"{item.Key}-{index}", Description: message)
                                        )
                                    ).ToArray()
                                );
                            }
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            result = Error.ServerNotResponse;
        }

        return result;
    }
}