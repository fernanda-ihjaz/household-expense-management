using System;
using HouseholdExpenseManagement.Domain.AggregatesModel.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseholdExpenseManagement.Infrastructure.EntityFramework.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(person => person.Id);

            builder.Property(person => person.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(person => person.Age)
                .IsRequired();
        }
    }
}
