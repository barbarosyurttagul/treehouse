using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace Barbarosoft.TreeHouse.WebApi.Middlewares;

public class LocalizationMiddleware
{
    private readonly ILogger<LocalizationMiddleware> _logger;
    private readonly RequestDelegate _next;

    public LocalizationMiddleware(ILogger<LocalizationMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        var browserLanguage = context.Request.Headers["Accept-Language"].ToString()[..2];
        _logger.LogInformation("Culture:" + browserLanguage);
        string? culture;
        switch (browserLanguage)
        {
            case "tr":
                culture = "tr-TR";
                break;
            default:
                culture = "en-US";
                break;
        }

        var requestCulture = new RequestCulture(culture);
        context.Features.Set<IRequestCultureFeature>(new RequestCultureFeature(requestCulture, null));

        CultureInfo.CurrentCulture = new CultureInfo(culture);
        CultureInfo.CurrentUICulture = new CultureInfo(culture);
        _logger.LogInformation("Current Culture:" + Thread.CurrentThread.CurrentCulture.Name);
        await _next.Invoke(context);
    }
}

public static class LocalizationMiddlewareExtensions
{
    public static IApplicationBuilder UseLocalization(this IApplicationBuilder app)
    {
        return app.UseMiddleware<LocalizationMiddleware>();
    }
}
