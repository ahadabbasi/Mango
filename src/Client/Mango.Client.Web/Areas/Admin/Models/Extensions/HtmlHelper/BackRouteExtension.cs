using Mango.Client.Web.Areas.Admin.Models.Configurations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Mango.Client.Web.Areas.Admin.Models.Extensions.HtmlHelper;

public static class BackRouteExtension
{
    public static void SetBackRoute(this IHtmlHelper helper, string routeName)
    {
        SetBackRoute(helper.ViewData, routeName);
    }

    public static void SetBackRoute<TModel>(this IHtmlHelper<TModel> helper, string routeName)
    {
        SetBackRoute(helper.ViewData, routeName);
    }
    
    public static bool BackRouteHasBeenSet(this IHtmlHelper helper)
    {
        return !string.IsNullOrEmpty(
            GetBackRoute(helper.ViewData)
        );
    }

    public static bool BackRouteHasBeenSet<TModel>(this IHtmlHelper<TModel> helper)
    {
        return !string.IsNullOrEmpty(
            GetBackRoute(helper.ViewData)
        );
    }

    public static string GetBackRoute(this IHtmlHelper helper)
    {
        return GetBackRoute(helper.ViewData);
    }

    public static string GetBackRoute<TModel>(this IHtmlHelper<TModel> helper)
    {
        return GetBackRoute(helper.ViewData);
    }

    private static void SetBackRoute(ViewDataDictionary viewData, string routeName)
    {
        viewData[Views.BackRouteName] = routeName;
    }
    
    private static string GetBackRoute(ViewDataDictionary viewData)
    {
        return viewData[Views.BackRouteName]?.ToString() ?? string.Empty;
    }
    
    public static void SetBackRouteTitle(this IHtmlHelper helper, string title)
    {
        SetBackRouteTitle(helper.ViewData, title);
    }

    public static void SetBackRouteTitle<TModel>(this IHtmlHelper<TModel> helper, string title)
    {
        SetBackRouteTitle(helper.ViewData, title);
    }
    
    public static bool BackRouteTitleHasBeenSet(this IHtmlHelper helper)
    {
        return !string.IsNullOrEmpty(
            GetBackRouteTitle(helper.ViewData)
        );
    }

    public static bool BackRouteTitleHasBeenSet<TModel>(this IHtmlHelper<TModel> helper)
    {
        return !string.IsNullOrEmpty(
            GetBackRouteTitle(helper.ViewData)
        );
    }

    public static string GetBackRouteTitle(this IHtmlHelper helper)
    {
        return GetBackRouteTitle(helper.ViewData);
    }

    public static string GetBackRouteTitle<TModel>(this IHtmlHelper<TModel> helper)
    {
        return GetBackRouteTitle(helper.ViewData);
    }

    private static void SetBackRouteTitle(ViewDataDictionary viewData, string title)
    {
        viewData[Views.BackRouteTitle] = title;
    }
    
    private static string GetBackRouteTitle(ViewDataDictionary viewData)
    {
        return viewData[Views.BackRouteTitle]?.ToString() ?? string.Empty;
    }
}