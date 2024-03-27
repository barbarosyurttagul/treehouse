using Barbarosoft.TreeHouse.Domain.Model;
using Barbarosoft.TreeHouse.Service;
using Barbarosoft.TreeHouse.WebApi.Controllers;
using Barbarosoft.TreeHouse.WebApi.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace Barbarosoft.TreeHouse.WebApi.Tests.Controllers.Courses;

[TestFixture(Category = "unit")]
internal class CreateCourseTests
{
    private readonly ICourseService _courseService;
    public CreateCourseTests()
    {
        _courseService = Substitute.For<ICourseService>();
    }

    [Test]
    public async Task CallsExpectedMethodsWithCorrectParameters()
    {
        // Arrange
        var courseName = "Programming";
        var categoryId = 1;
        var courseDto = new CreateCourseDto
        {
            Name = courseName,
            CategoryId = categoryId
        };
        var expectedCourse = new CourseEntity
        {
            Name = courseName,
            CategoryId = categoryId
        };
        var _coursesController = new CoursesController(_courseService);
        var httpContext = Substitute.For<HttpContext>();

        _coursesController.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };

        // Act
        var response = await _coursesController.Create(courseDto);

        // Assert
        await _courseService.Received(1).Create(Arg.Is<CourseEntity>(c =>
        c.Name == expectedCourse.Name && c.CategoryId == expectedCourse.CategoryId));
        Assert.That(response, Is.TypeOf<CreatedResult>());
    }

    [Test]
    public async Task ReturnsBadRequestWhenCourseNameIsNull()
    {
        // Arrange
        var categoryId = 1;
        var courseDto = new CreateCourseDto
        {
            CategoryId = categoryId
        };
        var _coursesController = new CoursesController(_courseService);

        // Act
        var response = await _coursesController.Create(courseDto);

        // Assert
        Assert.That(response, Is.TypeOf<BadRequestResult>());
    }

    [TestCase(0)]
    [TestCase(-1)]
    public async Task ReturnsBadRequestIfCategoryIdIsNotValid(int categoryId)
    {
        // Arrange
        var courseName = "Programming";
        var courseDto = new CreateCourseDto
        {
            Name = courseName,
            CategoryId = categoryId
        };
        var _coursesController = new CoursesController(_courseService);

        // Act
        var response = await _coursesController.Create(courseDto);

        // Assert
        Assert.That(response, Is.TypeOf<BadRequestResult>());
    }
}
