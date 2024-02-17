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
            Assert.That(result, Is.EqualTo("Valid"));
            Assert.That(result, Is.TypeOf<string>());
        }
    }

    internal class GetPluralTableNameFromEntity : RepositoryHelperTests
    {
        [Test]
        public void ReturnsPluralNamesWhenEntityEndsWithS()
        {
            // act
            var result = RepositoryHelper.GetPluralTableNameFromEntity<TossEntity>();

            // assert
            Assert.That(result, Is.EqualTo("Tosses"));
        }

        [Test]
        public void ReturnsPluralNamesWhenEntityEndsWithY()
        {
            // act
            var result = RepositoryHelper.GetPluralTableNameFromEntity<CategoryEntity>();

            // assert
            Assert.That(result, Is.EqualTo("Categories"));
        }

        [Test]
        public void ReturnsPluralNamesWhenDoesNotMatchInAllCases()
        {
            // act
            var result = RepositoryHelper.GetPluralTableNameFromEntity<ValidEntity>();

            //assert
            Assert.That(result, Is.EqualTo("Valids"));
        }
    }
    private class InvalidClass { }
    private class ValidEntity { }
    private class TossEntity { }
    private class CategoryEntity { }
}
