namespace Barbarosoft.TreeHouse.Repository.SqlRepository.Tests;

[TestFixture(Category = "unit")]
internal class RepositoryHelperTests
{
    internal class GetTableNameFromEntity : RepositoryHelperTests
    {
        [Test]
        public void ThrowsArgumentExceptionIfEntityNameNotEndsWithEntity()
        {
            // Act-Assert
            Assert.Throws<ArgumentException>(() =>
            {
                RepositoryHelper.GetTableNameFromEntity<InvalidClass>();
            });
        }

        [Test]
        public void ReturnsEntityNameWithoutEntitySuffix()
        {
            // Act
            var result = RepositoryHelper.GetTableNameFromEntity<ValidEntity>();

            // Assert
            Assert.That(result, Is.EqualTo("Vali"));
            Assert.That(result, Is.TypeOf<string>());
        }
    }

    private class InvalidClass { }
    private class ValidEntity { }
}
