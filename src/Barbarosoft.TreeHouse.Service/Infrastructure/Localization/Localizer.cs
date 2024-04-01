using Newtonsoft.Json;

namespace Barbarosoft.TreeHouse.Service.Infrastructure.Localization;

public class Localizer : ILocalizer
{
    private readonly JsonSerializer _serializer = new();
    private readonly IFileWrapper _fileWrapper;

    public Localizer(IFileWrapper fileWrapper)
    {
        _fileWrapper = fileWrapper;
    }
    public string GetString(string key)
    {
        var filePath = $"../Barbarosoft.TreeHouse.Service/Resources/{Thread.CurrentThread.CurrentCulture.Name}.json";
        var fullFilePath = Path.GetFullPath(filePath);

        if (_fileWrapper.Exists(fullFilePath))
        {
            var result = GetValueFromJSON(key, fullFilePath);
            return result;
        }

        return string.Empty;
    }

    private string GetValueFromJSON(string propertyName, string filePath)
    {
        if (string.IsNullOrEmpty(propertyName) || string.IsNullOrEmpty(filePath))
            return string.Empty;

        using FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        using StreamReader streamReader = new(stream);
        using JsonTextReader reader = new(streamReader);

        while (reader.Read())
        {
            if (reader.TokenType == JsonToken.PropertyName && reader.Value as string == propertyName)
            {
                reader.Read();
                return _serializer.Deserialize<string>(reader)!;
            }
        }

        return string.Empty;
    }
}
