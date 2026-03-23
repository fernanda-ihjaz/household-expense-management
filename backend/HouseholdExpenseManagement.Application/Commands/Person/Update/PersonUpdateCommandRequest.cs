using HouseholdExpenseManagement.Application.Models.Person;
using HouseholdExpenseManagement.Application.Results;
using MediatR;

namespace HouseholdExpenseManagement.Application.Commands.Person.Update
{
    public class PersonUpdateCommandRequest : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
        public PersonModel PersonModel { get; set; }

        public PersonUpdateCommandRequest(Guid id, PersonModel personModel)
        {
            Id = id;
            PersonModel = personModel;
        }
    }
}