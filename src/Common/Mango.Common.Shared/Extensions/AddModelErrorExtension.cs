using Mango.Common.Shared.Result;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Mango.Common.Shared.Extensions;

public static class AddModelErrorExtension
{
    public static void AddModelError(this ModelStateDictionary modelState, Error error)
    {
        modelState.AddModelError(
            error.Code,
            error.Description ?? string.Empty
        );
    }
}