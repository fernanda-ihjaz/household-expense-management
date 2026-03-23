using HouseholdExpenseManagement.Domain.AggregatesModel.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseholdExpenseManagement.Infrastructure.EntityFramework.Confirations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(category => category.Id);

            builder.Property(category => category.Description)
                .IsRequired()
                .HasMaxLength(400);

            builder.Property(category => category.Purpose)
                .IsRequired();
        }
    }
}
