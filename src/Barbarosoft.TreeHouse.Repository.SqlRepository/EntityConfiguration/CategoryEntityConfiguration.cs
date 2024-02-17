using Barbarosoft.TreeHouse.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barbarosoft.TreeHouse.Repository.SqlRepository.EntityConfiguration;

public sealed class CategoryEntityConfiguration : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.ToTable(RepositoryHelper.GetPluralTableNameFromEntity<CategoryEntity>());
        builder.HasKey(x => x.CategoryId);
    }
}