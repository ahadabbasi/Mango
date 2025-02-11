using Microsoft.AspNetCore.Mvc;

namespace Mango.Client.Web.Models.Extensions;

public static class RemoveControllerFromStringExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entry"></param>
    /// <returns></returns>
    public static string RemoveControllerFromString(this string entry)
    {
        return entry.Replace(nameof(Controller), string.Empty);
    }
}