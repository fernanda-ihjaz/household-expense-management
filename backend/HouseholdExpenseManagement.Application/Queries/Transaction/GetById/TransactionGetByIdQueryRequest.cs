using HouseholdExpenseManagement.Application.Models.Transaction;
using MediatR;

namespace HouseholdExpenseManagement.Application.Queries.Transaction.GetById
{
    public class TransactionGetByIdQueryRequest : IRequest<TransactionViewModel?>
    {
        public Guid Id { get; set; }

        public TransactionGetByIdQueryRequest(Guid id)
        {
            Id = id;
        }
    }
}