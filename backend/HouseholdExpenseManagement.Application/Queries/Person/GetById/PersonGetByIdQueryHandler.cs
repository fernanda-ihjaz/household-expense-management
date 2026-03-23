using HouseholdExpenseManagement.Application.Models.Person;
using HouseholdExpenseManagement.Domain.AggregatesModel.Person;
using MediatR;

namespace HouseholdExpenseManagement.Application.Queries.Person.GetById
{
    public class PersonGetByIdQueryHandler : IRequestHandler<PersonGetByIdQueryRequest, PersonViewModel?>
    {
        private readonly IPersonRepository _personRepository;

        public PersonGetByIdQueryHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<PersonViewModel?> Handle(PersonGetByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(request.Id);
            if (person is null)
                return null;

            return new PersonViewModel
            {
                Id = person.Id,
                Name = person.Name,
                Age = person.Age
            };
        }
    }
}