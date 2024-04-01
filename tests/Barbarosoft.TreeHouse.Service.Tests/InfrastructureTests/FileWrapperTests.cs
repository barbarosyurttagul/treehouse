using Barbarosoft.TreeHouse.Service.Infrastructure.Localization;

namespace Barbarosoft.TreeHouse.Service.Tests.InfrastructureTests;

[TestFixture(Category = "unit")]
public class FileWrapperTests
{
    [Test]
    public void ReturnsTrueWhenFileExists()
    {
        // Arrange
        var fileWrapper = new FileWrapper();
        var tempFilePath = Path.GetTempFileName();

        try
        {
            // Act
            var exists = fileWrapper.Exists(tempFilePath);

            // Assert
            Assert.IsTrue(exists);
        }
        finally
        {
            File.Delete(tempFilePath);
        }
    }

    [Test]
    public void ReturnsFalseWhenFileDoesNotExist()
    {
        // Arrange
        var fileWrapper = new FileWrapper();
        var nonExistentFilePath = "nonexistentfile.txt";

        // Act
        var exists = fileWrapper.Exists(nonExistentFilePath);

        // Assert
        Assert.IsFalse(exists);
    }
}
