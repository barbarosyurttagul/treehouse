namespace Barbarosoft.TreeHouse.WebApi.Middlewares.Extensions;
public static class LocalizationMiddlewareExtensions
{
    public static IApplicationBuilder UseLocalization(this IApplicationBuilder app)
    {
        return app.UseMiddleware<LocalizationMiddleware>();
    }
}
