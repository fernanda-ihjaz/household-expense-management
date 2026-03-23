using HouseholdExpenseManagement.Application.Models.Transaction;
using HouseholdExpenseManagement.Application.Results;
using MediatR;

namespace HouseholdExpenseManagement.Application.Commands.Transaction.Create
{
    public class TransactionCreateCommandRequest : IRequest<Result<Guid>>
    {
        public TransactionModel TransactionModel { get; set; }

        public TransactionCreateCommandRequest(TransactionModel transactionModel)
        {
            TransactionModel = transactionModel;
        }
    }
}