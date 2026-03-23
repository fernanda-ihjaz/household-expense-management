using HouseholdExpenseManagement.Domain.AggregatesModel.CategoryAggregate;
using HouseholdExpenseManagement.Domain.AggregatesModel.Person;
using HouseholdExpenseManagement.Domain.AggregatesModel.Transaction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseholdExpenseManagement.Infrastructure.EntityFramework.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(transaction => transaction.Id);

            builder.Property(transaction => transaction.Description)
                .IsRequired()
                .HasMaxLength(400);

            builder.Property(transaction => transaction.Amount)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(transaction => transaction.Type)
                .IsRequired();

            builder.HasOne<Person>()
                .WithMany()
                .HasForeignKey(transaction => transaction.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Category>()
                .WithMany()
                .HasForeignKey(transaction => transaction.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
