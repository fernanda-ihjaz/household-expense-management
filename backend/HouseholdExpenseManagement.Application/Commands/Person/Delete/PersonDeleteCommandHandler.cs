using HouseholdExpenseManagement.Application.Results;
using HouseholdExpenseManagement.Domain.AggregatesModel;
using HouseholdExpenseManagement.Domain.AggregatesModel.Person;
using HouseholdExpenseManagement.Domain.AggregatesModel.Transaction;
using MediatR;

namespace HouseholdExpenseManagement.Application.Commands.Person.Delete
{
    public class PersonDeleteCommandHandler : IRequestHandler<PersonDeleteCommandRequest, Result<bool>>
    {
        private readonly IPersonRepository _personRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PersonDeleteCommandHandler(
            IPersonRepository personRepository,
            ITransactionRepository transactionRepository,
            IUnitOfWork unitOfWork)
        {
            _personRepository = personRepository;
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(PersonDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var personId = request.Id;

                var person = await _personRepository.GetByIdAsync(personId);
                if (person is null)
                    return Result<bool>.Fail(string.Format("person not found with id '{0}'!", personId));

                var transactions = await _transactionRepository.GetByPersonIdAsync(personId);
                foreach (var transaction in transactions)
                    _transactionRepository.Delete(transaction);

                _personRepository.Delete(person);
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