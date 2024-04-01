using Microsoft.Extensions.Localization;

namespace Barbarosoft.TreeHouse.Service.Infrastructure.Localization;
public class JsonStringLocalizer : IStringLocalizer
{
    private readonly ILocalizer _localizer;
    public JsonStringLocalizer(ILocalizer localizer)
    {
        _localizer = localizer;
    }
    public LocalizedString this[string name]
    {
        get
        {
            var value = _localizer.GetString(name);
            if (value == string.Empty)
            {
                return new LocalizedString(name, value, resourceNotFound: true);
            }
            return new LocalizedString(name, value);
        }
    }

    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            var actualValue = this[name];
            return !actualValue.ResourceNotFound
                ? new LocalizedString(name, string.Format(actualValue.Value, arguments))
                : actualValue;
        }
    }

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        throw new NotImplementedException();
    }
}
