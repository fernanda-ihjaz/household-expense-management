using HouseholdExpenseManagement.Application.Models.Person;
using MediatR;

namespace HouseholdExpenseManagement.Application.Queries.Person.GetById
{
    public class PersonGetByIdQueryRequest : IRequest<PersonViewModel?>
    {
        public Guid Id { get; set; }

        public PersonGetByIdQueryRequest(Guid id)
        {
            Id = id;
        }
    }
}