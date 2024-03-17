using Barbarosoft.TreeHouse.Domain.Model;
using Barbarosoft.TreeHouse.Domain.Ports;
using NSubstitute;

namespace Barbarosoft.TreeHouse.Service.Tests;

[TestFixture(Category = "unit")]
public class CourseServiceTests
{
    readonly ICourseRepository _courseRepository;
    readonly CourseService _courseService;

    public CourseServiceTests()
    {
        _courseRepository = Substitute.For<ICourseRepository>();
        _courseService = new CourseService(_courseRepository);
    }

    [Test]
    public async Task CallsExpectedMethodsWhenGetAll()
    {
        // Act
        await _courseService.GetAll();

        // Assert
        await _courseRepository.Received(1).GetAll();
    }

    [Test]
    public async Task ReturnsAllCoursesWhenGetAll()
    {
        // Arrange
        var courseName = "C#";
        _courseRepository.GetAll().Returns(new CourseEntity[]{
            new CourseEntity{CourseId = 1, Name = courseName, CategoryId = 2}
        });

        // Act
        var courses = await _courseService.GetAll();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(courses, Has.Length.EqualTo(1));
            Assert.That(courses[0].Name, Is.EqualTo(courseName));
        });
    }

    [Test]
    public async Task ReturnsAllCoursesForGivenCategoryId()
    {
        // Arrange
        var courseName = "C#";
        var courseId = 1;
        var categoryId = 2;
        _courseRepository.GetByCategoryId(categoryId).Returns(new CourseEntity[]{
            new CourseEntity{CourseId = courseId, Name = courseName, CategoryId = 2}
        });

        // Act
        var courses = await _courseService.GetByCategoryId(categoryId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(courses, Has.Length.EqualTo(1));
            Assert.That(courses[0].Name, Is.EqualTo(courseName));
        });
    }

    [Test]
    public async Task CallsExpectedMethodsWhenCreatingCategory()
    {
        // Arrange
        int courseId = 1;
        int categoryId = 1;
        string courseName = "Programming";
        var course = new CourseEntity
        {
            CourseId = courseId,
            Name = courseName,
            CategoryId = categoryId
        };

        // Act
        await _courseService.Create(course);

        // Arrange
        await _courseRepository.Received(1).Create(course);
    }
}