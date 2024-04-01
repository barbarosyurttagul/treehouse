using Barbarosoft.TreeHouse.Service.Infrastructure.Localization;
using NSubstitute;

namespace Barbarosoft.TreeHouse.Service.Tests.InfrastructureTests;

[TestFixture(Category = "unit")]
public class JsonStringLocalizerTests
{
    private readonly ILocalizer _localizer;

    public JsonStringLocalizerTests()
    {
        _localizer = Substitute.For<ILocalizer>();
    }

    [Test]
    public void ReturnsLocalizedStringWithValidKey()
    {
        // Arrange
        var jsonStringLocalizer = new JsonStringLocalizer(_localizer);
        var expectedKey = "test_key";
        var expectedValue = "Test Value";
        _localizer.GetString(expectedKey).Returns(expectedValue);

        // Act
        var localizedString = jsonStringLocalizer[expectedKey];

        // Assert
        Assert.That(expectedKey, Is.EqualTo(localizedString.Name));
        Assert.That(expectedValue, Is.EqualTo(localizedString.Value));
    }

    [Test]
    public void ReturnsResourceNotFoundWithInValidKey()
    {
        // Arrange
        var jsonStringLocalizer = new JsonStringLocalizer(_localizer);
        var expectedKey = "test_key";
        _localizer.GetString(expectedKey).Returns(string.Empty);

        // Act
        var localizedString = jsonStringLocalizer[expectedKey];

        // Assert
        Assert.That(localizedString.ResourceNotFound, Is.EqualTo(true));
    }

    [Test]
    public void Indexer_WithArguments_ReturnsLocalizedStringWithArguments()
    {
        // Arrange
        var jsonStringLocalizer = new JsonStringLocalizer(_localizer);
        var keyWithArgs = "test_key_with_args";
        var arguments = new object[] { "arg1", "arg2" };
        var expectedValue = "Test Value with arg1 and arg2";
        _localizer.GetString(keyWithArgs).Returns(expectedValue);

        // Act
        var localizedString = jsonStringLocalizer[keyWithArgs, arguments];

        // Assert
        Assert.That(expectedValue, Is.EqualTo(localizedString.Value));
    }

    [Test]
    public void GetAllStrings_ThrowsNotImplementedException()
    {
        // Arrange
        var jsonStringLocalizer = new JsonStringLocalizer(_localizer);

        // Act & Assert
        Assert.Throws<NotImplementedException>(() => jsonStringLocalizer.GetAllStrings(false));
    }
}
