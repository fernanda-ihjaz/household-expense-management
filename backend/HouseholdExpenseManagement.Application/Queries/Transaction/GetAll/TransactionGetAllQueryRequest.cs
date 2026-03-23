using HouseholdExpenseManagement.Application.Models.Person;
using HouseholdExpenseManagement.Application.Models.Transaction;
using MediatR;

namespace HouseholdExpenseManagement.Application.Queries.Transaction.GetAll
{
    public class TransactionGetAllQueryRequest : IRequest<IEnumerable<TransactionViewModel>> { }
}