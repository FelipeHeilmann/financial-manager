using FinancialManager.Application.Abstraction;
using FinancialManager.Application.Data;
using FinancialManager.Domain.Entity;
using FinancialManager.Domain.Enum;
using FinancialManager.Domain.Repository;
using FinancialManager.Domain.Abstraction;
using FinancialManager.Domain.Exception;

namespace FinancialManager.Application.Usecase.CreateTransaction
{
    public sealed class CreateTransactionCommandHandler : ICommandHandler<CreateTransactionCommand, Result<Guid>>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTransactionCommandHandler(ITransactionRepository transactionRepository, IUnitOfWork unitOfWork)
        {
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateTransactionCommand command, CancellationToken cancellationToken)
        {
            if (command.request.Type > 2) return Result.Failure<Guid>(TransactionErrors.InvalidType);
            var transactionType = command.request.Type == 1 ? TransactionType.Credit : TransactionType.Deposit;
            var transaction = Transaction.Create(command.request.Author, command.request.Amount, command.request.Date, transactionType, command.request.Description);

            await _transactionRepository.SaveAsync(transaction, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(transaction.Id);
        }
    }
}
