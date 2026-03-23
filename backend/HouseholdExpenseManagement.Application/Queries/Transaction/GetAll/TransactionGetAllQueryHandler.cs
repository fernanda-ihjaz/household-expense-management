using HouseholdExpenseManagement.Application.Models.Person;
using HouseholdExpenseManagement.Application.Models.Transaction;
using HouseholdExpenseManagement.Domain.AggregatesModel.Person;
using HouseholdExpenseManagement.Domain.AggregatesModel.Transaction;
using MediatR;

namespace HouseholdExpenseManagement.Application.Queries.Transaction.GetAll
{
    public class TransactionGetAllQueryHandler : IRequestHandler<TransactionGetAllQueryRequest, IEnumerable<TransactionViewModel>>
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionGetAllQueryHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<IEnumerable<TransactionViewModel>> Handle(TransactionGetAllQueryRequest request, CancellationToken cancellationToken)
        {
            var transactions = await _transactionRepository.GetAllAsync();

            if (!transactions.Any())
            {
                return [];
            }

            return transactions.Select(p => new TransactionViewModel
            {
                Id = p.Id,
                Description = p.Description,
                Amount = p.Amount,
                Type = p.Type.ToString(),
                PersonId = p.PersonId,
                CategoryId = p.CategoryId
            });
        }
    }
}