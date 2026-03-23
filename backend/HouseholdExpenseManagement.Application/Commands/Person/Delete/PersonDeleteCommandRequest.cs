using HouseholdExpenseManagement.Application.Results;
using MediatR;

namespace HouseholdExpenseManagement.Application.Commands.Person.Delete
{
    public class PersonDeleteCommandRequest : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }

        public PersonDeleteCommandRequest(Guid id)
        {
            Id = id;
        }
    }
}