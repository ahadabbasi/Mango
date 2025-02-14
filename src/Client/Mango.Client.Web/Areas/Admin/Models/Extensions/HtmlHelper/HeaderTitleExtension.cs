using Mango.Client.Web.Areas.Admin.Models.Configurations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Mango.Client.Web.Areas.Admin.Models.Extensions.HtmlHelper;

public static class HeaderTitleExtension
{
    public static void SetHeaderTitle(this IHtmlHelper helper, string title)
    {
        SetHeaderTitle(helper.ViewData, title);
    }

    public static void SetHeaderTitle<TModel>(this IHtmlHelper<TModel> helper, string title)
    {
        SetHeaderTitle(helper.ViewData, title);
    }
    
    public static bool HeaderTitleHasBeenSet(this IHtmlHelper helper)
    {
        return !string.IsNullOrEmpty(
            GetHeaderTitle(helper.ViewData)
        );
    }

    public static bool HeaderTitleHasBeenSet<TModel>(this IHtmlHelper<TModel> helper)
    {
        return !string.IsNullOrEmpty(
            GetHeaderTitle(helper.ViewData)
        );
    }

    public static string GetHeaderTitle(this IHtmlHelper helper)
    {
        return GetHeaderTitle(helper.ViewData);
    }

    public static string GetHeaderTitle<TModel>(this IHtmlHelper<TModel> helper)
    {
        return GetHeaderTitle(helper.ViewData);
    }

    private static void SetHeaderTitle(ViewDataDictionary viewData, string title)
    {
        viewData[Views.HeaderTitle] = title;
    }

    private static string GetHeaderTitle(ViewDataDictionary viewData)
    {
        return viewData[Views.HeaderTitle]?.ToString() ?? string.Empty;
    }
}