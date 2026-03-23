using HouseholdExpenseManagement.Application.Models.Transaction;
using HouseholdExpenseManagement.Domain.AggregatesModel.Transaction;
using MediatR;

namespace HouseholdExpenseManagement.Application.Queries.Transaction.GetByPersonId
{
    public class TransactionGetByPersonIdQueryHandler : IRequestHandler<TransactionGetByPersonIdQueryRequest, IEnumerable<TransactionViewModel>>
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionGetByPersonIdQueryHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<IEnumerable<TransactionViewModel>> Handle(TransactionGetByPersonIdQueryRequest request, CancellationToken cancellationToken)
        {
            var transactions = await _transactionRepository.GetByPersonIdAsync(request.PersonId);
            return transactions.Select(t => new TransactionViewModel
            {
                Id = t.Id,
                Description = t.Description,
                Amount = t.Amount,
                Type = t.Type.ToString(),
                PersonId = t.PersonId,
                CategoryId = t.CategoryId
            });
        }
    }
}