using FinancialManager.Application.Abstraction;
using FinancialManager.Application.Data;
using FinancialManager.Domain.Abstraction;
using FinancialManager.Domain.Exception;
using FinancialManager.Domain.Repository;
using MediatR;

namespace FinancialManager.Application.Usecase.Transaction.DeleteTransaction
{
    public sealed class DeleteTransactionCommandHandler : ICommandHandler<DeleteTransactionCommand, Result>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTransactionCommandHandler(ITransactionRepository transactionRepository, IUnitOfWork unitOfWork)
        {
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteTransactionCommand command, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetByIdAsync(command.id, cancellationToken);
            if (transaction == null)
            {
                return Result.Failure(TransactionErrors.NotFound);
            }

            _transactionRepository.Delete(transaction, cancellationToken);

            await _unitOfWork.Commit();

            return Result.Success();
        }
    }
}
