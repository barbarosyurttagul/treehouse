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
}