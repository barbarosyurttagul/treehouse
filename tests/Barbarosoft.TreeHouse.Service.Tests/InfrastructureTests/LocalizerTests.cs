using System.Text;
using Barbarosoft.TreeHouse.Service.Infrastructure.Localization;
using NSubstitute;

namespace Barbarosoft.TreeHouse.Service.Tests.InfrastructureTests;

[TestFixture(Category = "unit")]
public class LocalizerTests
{
    [Test]
    public void ReturnsEmptyStringWhenFileDoesNotExist()
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

    [Test]
    public void ReturnsExpectedValueWhenFileExists()
    {
        // Arrange
        var expectedKey = "test_key";
        var expectedValue = "Test Value";
        var jsonContent = "{\"test_key\": \"Test Value\"}";
        var filePath = $"Resources/{Thread.CurrentThread.CurrentCulture.Name}.json";
        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Resources");
        var fullFilePath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
        using (FileStream fs = File.Create(fullFilePath))
        {
            byte[] info = new UTF8Encoding(true).GetBytes(jsonContent);
            fs.Write(info, 0, info.Length);
        }

        var fileWrapper = Substitute.For<IFileWrapper>();

        var localizer = new Localizer(fileWrapper);
        fileWrapper.Exists(fullFilePath).Returns(true);

        // Act
        var actualValue = localizer.GetString(expectedKey);

        // Assert
        Assert.That(actualValue, Is.EqualTo(expectedValue));

        File.Delete(fullFilePath);
        Directory.Delete(Directory.GetCurrentDirectory() + "/Resources");
    }

    [Test]
    public void ReturnsEmptyStringWhenNoMatchingKeyInFile()
    {
        // Arrange
        var expectedKey = "test_key";
        var jsonContent = "{\"wrong_key\": \"Test Value\"}";
        var filePath = $"Resources/{Thread.CurrentThread.CurrentCulture.Name}.json";
        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Resources");
        var fullFilePath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
        using (FileStream fs = File.Create(fullFilePath))
        {
            byte[] info = new UTF8Encoding(true).GetBytes(jsonContent);
            fs.Write(info, 0, info.Length);
        }

        var fileWrapper = Substitute.For<IFileWrapper>();

        var localizer = new Localizer(fileWrapper);
        fileWrapper.Exists(fullFilePath).Returns(true);

        // Act
        var actualValue = localizer.GetString(expectedKey);

        // Assert
        Assert.That(actualValue, Is.EqualTo(string.Empty));

        File.Delete(fullFilePath);
        Directory.Delete(Directory.GetCurrentDirectory() + "/Resources");
    }

    [TestCase(null!)]
    [TestCase("")]
    public void ReturnsEmptyStringWhenKeyIsNullOrEmpty(string expectedKey)
    {
        // Arrange
        var jsonContent = "{\"test_key\": \"Test Value\"}";
        var filePath = $"Resources/{Thread.CurrentThread.CurrentCulture.Name}.json";
        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Resources");
        var fullFilePath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
        using (FileStream fs = File.Create(fullFilePath))
        {
            byte[] info = new UTF8Encoding(true).GetBytes(jsonContent);
            fs.Write(info, 0, info.Length);
        }

        var fileWrapper = Substitute.For<IFileWrapper>();

        var localizer = new Localizer(fileWrapper);
        fileWrapper.Exists(fullFilePath).Returns(true);

        // Act
        var actualValue = localizer.GetString(expectedKey);

        // Assert
        Assert.That(actualValue, Is.EqualTo(string.Empty));

        File.Delete(fullFilePath);
        Directory.Delete(Directory.GetCurrentDirectory() + "/Resources");
    }
}
