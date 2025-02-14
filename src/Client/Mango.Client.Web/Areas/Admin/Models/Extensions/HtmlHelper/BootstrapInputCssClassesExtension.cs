using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mango.Client.Web.Areas.Admin.Models.Extensions.HtmlHelper;

public static class BootstrapInputCssClassesExtension
{
    public static string BootstrapInputCssClasses<TModel, TValue>(
        this IHtmlHelper<TModel> html,
        Expression<Func<TModel, TValue>> expression,
        params string[]? classes
    )
    {
        IList<string> result = new List<string>()
        {
            "form-control"
        };

        if (classes != null && classes.Any())
        {
            foreach (string @class in classes)
            {
                result.Add(@class);
            }
        }

        if (!html.ViewData.ModelState.IsValid)
        {
            if (expression.Body is MemberExpression memberExpression)
            {
                string propertyName = memberExpression.Member.Name;
                
                if (html.ViewData.ModelState.Keys.Contains(propertyName))
                {
                    //ViewData.ModelState.Keys.Contains()
                    result.Add("is-invalid");
                }
            }
            
        }
        
        return string.Join((char)32, result);
    }
}