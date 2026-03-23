using HouseholdExpenseManagement.Application.Results;
using MediatR;

namespace HouseholdExpenseManagement.Application.Commands.Transaction.Delete
{
    public class TransactionDeleteCommandRequest : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }

        public TransactionDeleteCommandRequest(Guid id)
        {
            Id = id;
        }
    }
}