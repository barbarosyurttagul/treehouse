using Barbarosoft.TreeHouse.Service.Infrastructure.Localization;
using Microsoft.Extensions.Localization;

namespace Barbarosoft.TreeHouse.WebApi.ServiceRegistrations;

public static class LocalizationService
{
    public static void AddLocalizationSupport(this IServiceCollection services)
    {
        services.AddLocalization();
        services.AddSingleton<IStringLocalizer, JsonStringLocalizer>();
    }
}
