using HouseholdExpenseManagement.Application.Models.Person;
using HouseholdExpenseManagement.Domain.AggregatesModel.Person;
using MediatR;

namespace HouseholdExpenseManagement.Application.Queries.Person.GetAll
{
    public class PersonGetAllQueryHandler : IRequestHandler<PersonGetAllQueryRequest, IEnumerable<PersonViewModel>>
    {
        private readonly IPersonRepository _personRepository;

        public PersonGetAllQueryHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<IEnumerable<PersonViewModel>> Handle(PersonGetAllQueryRequest request, CancellationToken cancellationToken)
        {
            var persons = await _personRepository.GetAllAsync();

            if (!persons.Any())
            {
                return [];
            }

            return persons.Select(p => new PersonViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Age = p.Age
            });
        }
    }
}