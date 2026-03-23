using HouseholdExpenseManagement.Domain.AggregatesModel.Transaction;

namespace HouseholdExpenseManagement.Domain.AggregatesModel.CategoryAggregate
{
    public class Category : IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public CategoryPurpose Purpose { get; private set; }

        public Category(string description, CategoryPurpose purpose)
        {
            Id = Guid.NewGuid();
            SetDescription(description);
            Purpose = purpose;
        }

        public Category(string description, int purposeId)
        {
            Id = Guid.NewGuid();
            SetDescription(description);
            Purpose = (CategoryPurpose)purposeId;
        }

        public void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description) || description.Length > 400)
                throw new ArgumentException("Invalid description");

            Description = description;
        }

        public bool Allows(TransactionType type)
        {
            return Purpose == CategoryPurpose.Both
                || Purpose == CategoryPurpose.Expense && type == TransactionType.Expense
                || Purpose == CategoryPurpose.Income && type == TransactionType.Income;
        }
    }
}
