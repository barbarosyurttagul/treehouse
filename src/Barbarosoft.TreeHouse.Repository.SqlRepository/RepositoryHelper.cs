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

    public static string GetPluralTableNameFromEntity<T>()
    {
        var name = GetTableNameFromEntity<T>();
        if (name.EndsWith('s'))
            return $"{name}es";
        else if (name.EndsWith('y'))
            return $"{name.Substring(0, name.Length - 1)}ies";
        return $"{name}s";
    }
}
