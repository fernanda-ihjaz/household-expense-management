using HouseholdExpenseManagement.Domain.AggregatesModel.CategoryAggregate;
using HouseholdExpenseManagement.Domain.AggregatesModel.Person;
using HouseholdExpenseManagement.Domain.AggregatesModel.Transaction;
using HouseholdExpenseManagement.Domain.AggregatesModel;
using Microsoft.EntityFrameworkCore;

namespace HouseholdExpenseManagement.Infrastructure.EntityFramework
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
