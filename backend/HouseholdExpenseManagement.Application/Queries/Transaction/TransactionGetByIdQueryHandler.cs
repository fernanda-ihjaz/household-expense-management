using HouseholdExpenseManagement.Application.Models.Transaction;
using HouseholdExpenseManagement.Domain.AggregatesModel.Transaction;
using MediatR;

namespace HouseholdExpenseManagement.Application.Queries.Transaction.GetById
{
    public class TransactionGetByIdQueryHandler : IRequestHandler<TransactionGetByIdQueryRequest, TransactionViewModel?>
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionGetByIdQueryHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<TransactionViewModel?> Handle(TransactionGetByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetByIdAsync(request.Id);
            if (transaction is null)
                return null;

            return new TransactionViewModel
            {
                Id = transaction.Id,
                Description = transaction.Description,
                Amount = transaction.Amount,
                Type = transaction.Type.ToString(),
                PersonId = transaction.PersonId,
                CategoryId = transaction.CategoryId
            };
        }
    }
}