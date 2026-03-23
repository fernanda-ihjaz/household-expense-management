using HouseholdExpenseManagement.Application.Results;
using HouseholdExpenseManagement.Domain.AggregatesModel;
using HouseholdExpenseManagement.Domain.AggregatesModel.Person;
using MediatR;

namespace HouseholdExpenseManagement.Application.Commands.Person.Update
{
    public class PersonUpdateCommandHandler : IRequestHandler<PersonUpdateCommandRequest, Result<bool>>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PersonUpdateCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork)
        {
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(PersonUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var personId = request.Id;

                var person = await _personRepository.GetByIdAsync(personId);
                if (person is null)
                    return Result<bool>.Fail(string.Format("person not found with id '{0}'!", personId));

                person.SetName(request.PersonModel.Name);
                person.SetAge(request.PersonModel.Age);

                _personRepository.Update(person);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail(ex.Message);
            }
        }
    }
}