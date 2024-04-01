using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace Barbarosoft.TreeHouse.WebApi.Middlewares;

public sealed class LocalizationMiddleware
{
    private readonly RequestDelegate _next;
    private string? _culture;

    public string Culture { get { return _culture!; } }

    public LocalizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        var browserLanguage = context.Request.Headers["Accept-Language"].ToString()[..2];
        switch (browserLanguage)
        {
            case "tr":
                _culture = "tr-TR";
                break;
            default:
                _culture = "en-US";
                break;
        }

        var requestCulture = new RequestCulture(_culture);
        context.Features.Set<IRequestCultureFeature>(new RequestCultureFeature(requestCulture, null));

        CultureInfo.CurrentCulture = new CultureInfo(_culture);
        CultureInfo.CurrentUICulture = new CultureInfo(_culture);
        await _next.Invoke(context);
    }
}
