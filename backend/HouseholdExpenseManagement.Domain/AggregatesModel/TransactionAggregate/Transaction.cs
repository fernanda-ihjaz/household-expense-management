namespace HouseholdExpenseManagement.Domain.AggregatesModel.Transaction
{
    public class Transaction : IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public decimal Amount { get; private set; }
        public TransactionType Type { get; private set; }

        public Guid PersonId { get; private set; }
        public Guid CategoryId { get; private set; }

        private Transaction() { } // EF

        public Transaction(string description, decimal amount, TransactionType type, Guid personId, Guid categoryId, bool isPersonMinor, bool categoryAllows)
        {
            Id = Guid.NewGuid();

            SetDescription(description);
            SetAmount(amount);

            if (isPersonMinor && type == TransactionType.Income)
                throw new InvalidOperationException("Minor cannot have income");

            if (!categoryAllows)
                throw new InvalidOperationException("Category does not allow this transaction type");

            Type = type;
            PersonId = personId;
            CategoryId = categoryId;
        }

        private void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description) || description.Length > 400)
                throw new ArgumentException("Invalid description");

            Description = description;
        }

        private void SetAmount(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero");

            Amount = amount;
        }
    }
}
