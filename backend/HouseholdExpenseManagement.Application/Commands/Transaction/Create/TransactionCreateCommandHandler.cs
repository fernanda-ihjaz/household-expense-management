using HouseholdExpenseManagement.Application.Results;
using HouseholdExpenseManagement.Domain.AggregatesModel;
using HouseholdExpenseManagement.Domain.AggregatesModel.CategoryAggregate;
using HouseholdExpenseManagement.Domain.AggregatesModel.Person;
using HouseholdExpenseManagement.Domain.AggregatesModel.Transaction;
using MediatR;
using _Transaction = HouseholdExpenseManagement.Domain.AggregatesModel.Transaction.Transaction;

namespace HouseholdExpenseManagement.Application.Commands.Transaction.Create
{
    public class TransactionCreateCommandHandler : IRequestHandler<TransactionCreateCommandRequest, Result<Guid>>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IPersonRepository _personRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionCreateCommandHandler(
            ITransactionRepository transactionRepository,
            IPersonRepository personRepository,
            ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork)
        {
            _transactionRepository = transactionRepository;
            _personRepository = personRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(TransactionCreateCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var transactionModel = request.TransactionModel;

                var person = await _personRepository.GetByIdAsync(transactionModel.PersonId);
                if (person is null)
                    return Result<Guid>.Fail(string.Format("person not found with id '{0}'!", transactionModel.PersonId));

                var category = await _categoryRepository.GetByIdAsync(transactionModel.CategoryId);
                if (category is null)
                    return Result<Guid>.Fail(string.Format("category not found with id '{0}'!", transactionModel.CategoryId));

                var transaction = new _Transaction(
                    description: transactionModel.Description,
                    amount: transactionModel.Amount,
                    type: transactionModel.Type,
                    personId: person.Id,
                    categoryId: category.Id,
                    isPersonMinor: person.IsMinor(),
                    categoryAllows: category.Allows(transactionModel.Type)
                );

                await _transactionRepository.AddAsync(transaction);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<Guid>.Ok(transaction.Id);
            }
            catch (Exception ex)
            {
                return Result<Guid>.Fail(ex.Message);
            }
        }
    }
}