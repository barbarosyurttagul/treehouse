using Barbarosoft.TreeHouse.Domain.Model;
using Barbarosoft.TreeHouse.Domain.Ports;
using Microsoft.Extensions.Localization;
using NSubstitute;

namespace Barbarosoft.TreeHouse.Service.Tests;

[TestFixture(Category = "unit")]
public class CourseServiceTests
{
    private const string CourseName = "Programming";
    private const int CourseId = 1;
    private const int CategoryId = 1;
    readonly ICourseRepository _courseRepository;
    readonly CourseService _courseService;
    readonly IStringLocalizer _localizer;

    public CourseServiceTests()
    {
        _courseRepository = Substitute.For<ICourseRepository>();
        _localizer = Substitute.For<IStringLocalizer>();
        _courseService = new CourseService(_courseRepository, _localizer);
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
        _courseRepository.GetAll().Returns(new CourseEntity[]{
            new CourseEntity{CourseId = CourseId, Name = CourseName, CategoryId = CategoryId}
        });

        // Act
        var courses = await _courseService.GetAll();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(courses, Has.Length.EqualTo(1));
            Assert.That(courses[0].Name, Is.EqualTo(CourseName));
        });
    }

    [Test]
    public async Task ReturnsAllCoursesForGivenCategoryId()
    {
        // Arrange
        _courseRepository.GetByCategoryId(CategoryId).Returns(new CourseEntity[]{
            new CourseEntity{CourseId = CourseId, Name = CourseName, CategoryId = CategoryId}
        });

        // Act
        var courses = await _courseService.GetByCategoryId(CategoryId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(courses, Has.Length.EqualTo(1));
            Assert.That(courses[0].Name, Is.EqualTo(CourseName));
        });
    }

    [Test]
    public async Task CallsExpectedMethodsWhenCreatingCategory()
    {
        // Arrange
        var course = new CourseEntity
        {
            CourseId = CourseId,
            Name = CourseName,
            CategoryId = CategoryId
        };

        // Act
        await _courseService.Create(course);

        // Arrange
        await _courseRepository.Received(1).Create(course);
    }

    [TestCase("", 1, "Course name can not be empty")]
    [TestCase(CourseName, 0, "Category Id can not be equal or less than 0")]
    [TestCase(CourseName, -1, "Category Id can not be equal or less than 0")]
    public async Task ReturnsServiceResultWithCorrectMessageForInvalidEntries(string courseName, int categoryId, string expectedErrorMessage)
    {
        // Arrange
        var course = new CourseEntity
        {
            Name = courseName,
            CategoryId = categoryId
        };

        _localizer["course_name_can_not_be_empty"].Returns(new LocalizedString("course_name_can_not_be_empty", expectedErrorMessage));
        _localizer["category_id_invalid"].Returns(new LocalizedString("category_id_invalid", expectedErrorMessage));
        // Act
        var result = await _courseService.Create(course);

        // Arrange
        Assert.That(result.Message, Is.EqualTo(expectedErrorMessage));
    }
}
