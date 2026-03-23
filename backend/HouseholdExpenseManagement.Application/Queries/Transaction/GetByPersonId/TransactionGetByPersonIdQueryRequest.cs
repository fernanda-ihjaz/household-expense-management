using HouseholdExpenseManagement.Application.Models.Transaction;
using MediatR;

namespace HouseholdExpenseManagement.Application.Queries.Transaction.GetByPersonId
{
    public class TransactionGetByPersonIdQueryRequest : IRequest<IEnumerable<TransactionViewModel>>
    {
        public Guid PersonId { get; set; }

        public TransactionGetByPersonIdQueryRequest(Guid personId)
        {
            PersonId = personId;
        }
    }
}