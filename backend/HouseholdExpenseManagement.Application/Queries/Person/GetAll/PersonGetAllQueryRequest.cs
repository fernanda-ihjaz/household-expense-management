using HouseholdExpenseManagement.Application.Models.Person;
using MediatR;

namespace HouseholdExpenseManagement.Application.Queries.Person.GetAll
{
    public class PersonGetAllQueryRequest : IRequest<IEnumerable<PersonViewModel>> { }
}