using HouseholdExpenseManagement.Application.Models.Person;
using HouseholdExpenseManagement.Application.Results;
using MediatR;

namespace HouseholdExpenseManagement.Application.Commands.Person.Create
{
    public class PersonCreateCommandRequest : IRequest<Result<Guid>>
    {
        public PersonModel PersonModel { get; set; }

        public PersonCreateCommandRequest(PersonModel personModel)
        {
            PersonModel = personModel;
        }
    }
}