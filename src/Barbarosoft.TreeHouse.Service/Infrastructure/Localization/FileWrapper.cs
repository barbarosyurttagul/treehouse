namespace Barbarosoft.TreeHouse.Service.Infrastructure.Localization;

public class FileWrapper : IFileWrapper
{
    public bool Exists(string path)
    {
        return File.Exists(path);
    }
}
