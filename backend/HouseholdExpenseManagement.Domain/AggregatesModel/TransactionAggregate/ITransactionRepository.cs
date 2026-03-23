namespace HouseholdExpenseManagement.Domain.AggregatesModel.Transaction
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetAllAsync();
        Task<Transaction?> GetByIdAsync(Guid id);
        Task<IEnumerable<Transaction>> GetByPersonIdAsync(Guid personId);
        Task<IEnumerable<Transaction>> GetByCategoryIdAsync(Guid categoryId);
        Task AddAsync(Transaction transaction);
        void Update(Transaction transaction);
        void Delete(Transaction transaction);
    }
}
