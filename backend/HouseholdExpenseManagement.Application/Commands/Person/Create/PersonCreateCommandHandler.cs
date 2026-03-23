using HouseholdExpenseManagement.Application.Results;
using HouseholdExpenseManagement.Domain.AggregatesModel;
using HouseholdExpenseManagement.Domain.AggregatesModel.CategoryAggregate;
using HouseholdExpenseManagement.Domain.AggregatesModel.Person;
using MediatR;
using _Person = HouseholdExpenseManagement.Domain.AggregatesModel.Person.Person;

namespace HouseholdExpenseManagement.Application.Commands.Person.Create
{
    public class PersonCreateCommandHandler : IRequestHandler<PersonCreateCommandRequest, Result<Guid>>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PersonCreateCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork)
        {
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(PersonCreateCommandRequest request, CancellationToken cancellationToken)
        {
            var model = request.PersonModel;
            try
            {
                var person = new _Person(
                    model.Name, 
                    model.Age
                    );

                await _personRepository.AddAsync(person);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<Guid>.Ok(person.Id);
            }
            catch (Exception ex)
            {
                return Result<Guid>.Fail(ex.Message);
            }

        }
    }
}