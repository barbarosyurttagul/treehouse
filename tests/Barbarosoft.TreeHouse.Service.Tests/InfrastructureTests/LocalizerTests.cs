using Barbarosoft.TreeHouse.Service.Infrastructure.Localization;
using NSubstitute;

namespace Barbarosoft.TreeHouse.Service.Tests.InfrastructureTests;

[TestFixture(Category = "unit")]
public class LocalizerTests
{
    [Test]
    public void GetString_WhenFileDoesNotExist_ReturnsEmptyString()
    {
        // Arrange
        var expectedKey = "test_key";
        var fileWrapper = Substitute.For<IFileWrapper>();
        var localizer = new Localizer(fileWrapper);

        fileWrapper.Exists(Arg.Any<string>()).Returns(false);

        // Act
        var actualValue = localizer.GetString(expectedKey);

        // Assert
        Assert.That(actualValue, Is.EqualTo(string.Empty));
    }
}
