using HouseholdExpenseManagement.Application.Results;
using HouseholdExpenseManagement.Domain.AggregatesModel;
using HouseholdExpenseManagement.Domain.AggregatesModel.Transaction;
using MediatR;

namespace HouseholdExpenseManagement.Application.Commands.Transaction.Delete
{
    public class TransactionDeleteCommandHandler : IRequestHandler<TransactionDeleteCommandRequest, Result<bool>>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionDeleteCommandHandler(ITransactionRepository transactionRepository, IUnitOfWork unitOfWork)
        {
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(TransactionDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var transactionId = request.Id;

                var transaction = await _transactionRepository.GetByIdAsync(transactionId);
                if (transaction is null)
                    return Result<bool>.Fail(string.Format("transaction not found with id '{0}'!", transactionId));

                _transactionRepository.Delete(transaction);
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