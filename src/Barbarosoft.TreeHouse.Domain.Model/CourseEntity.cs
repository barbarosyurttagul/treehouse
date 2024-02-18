namespace Barbarosoft.TreeHouse.Domain.Model;

public class CourseEntity
{
    public int CourseId { get; set; }
    public string Name { get; set; } = string.Empty;

    public int CategoryId { get; set; }

}
