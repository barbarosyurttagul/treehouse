namespace Barbarosoft.TreeHouse.Repository.SqlRepository;

public static class RepositoryHelper
{
    public static string GetTableNameFromEntity<T>()
    {
        if (!typeof(T).Name.EndsWith("Entity"))
        {
            throw new ArgumentException("The [Type] does not represent an entity");
        }
        return typeof(T).Name[..^6];
    }
}
