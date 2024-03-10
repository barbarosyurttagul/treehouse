using Barbarosoft.TreeHouse.Domain.Model;
using Barbarosoft.TreeHouse.Domain.Ports;
using NSubstitute;

namespace Barbarosoft.TreeHouse.Service.Tests;

[TestFixture(Category = "unit")]
internal class InstructorServiceTests
{
    readonly IInstructorRepository _instructorRepository;
    readonly InstructorService _instructorService;

    public InstructorServiceTests()
    {
        _instructorRepository = Substitute.For<IInstructorRepository>();
        _instructorService = new InstructorService(_instructorRepository);
    }

    [Test]
    public async Task CallsExpectedMethodsWhenGetAll()
    {
        // Act
        await _instructorService.GetAll();

        // Assert
        await _instructorRepository.Received(1).ListAsync();
    }

    [Test]
    public async Task ReturnsAllInstructorsWhenGetAll()
    {
        var instructorId = 1;
        var firstName = "Test_First";
        var lastName = "Test_Last";
        _instructorRepository.ListAsync().Returns(new InstructorEntity[]{
            new InstructorEntity
            {
                InstructorId = instructorId,
                FirstName = firstName,
                LastName = lastName
            }
        });

        // Act
        var instructors = await _instructorService.GetAll();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(instructors, Has.Length.EqualTo(1));
            Assert.That(instructors[0].FirstName, Is.EqualTo(firstName));
        });
    }

    [Test]
    public async Task ReturnsInstructorByGivenInstructorId()
    {
        // Assert
        int instructorId = 1;
        var firstName = "Test_First";
        var lastName = "Test_Last";
        var instructor = new InstructorEntity
        {
            InstructorId = instructorId,
            FirstName = firstName,
            LastName = lastName
        };
        _instructorRepository.GetById(instructorId).Returns(instructor);

        // Act
        var result = await _instructorService.GetById(instructorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.TypeOf<InstructorEntity>());
            Assert.That(result.FirstName, Is.EqualTo(firstName));
        });
    }

    [Test]
    public async Task ReturnsCoursesOfInstructor()
    {
        // Arrange
        var instructorId = 1;
        var courseName = "Programming";
        var categoryId = 1;
        var courseId = 1;
        _instructorRepository.GetCoursesOfInstructor(instructorId)
        .Returns(new CourseEntity[]{
            new CourseEntity
            {
                Name = courseName,
                CategoryId = categoryId,
                CourseId = courseId
            }
        });

        // Act
        var courses = await _instructorService.GetCoursesOfInstructor(instructorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(courses, Is.TypeOf<CourseEntity[]>());
            Assert.That(courses, Has.Length.EqualTo(1));
        });

    }
}