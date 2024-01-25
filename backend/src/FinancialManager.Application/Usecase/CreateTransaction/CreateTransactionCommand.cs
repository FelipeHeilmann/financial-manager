using FinancialManager.Application.Abstraction;
using FinancialManager.Application.Data;
using FinancialManager.Application.Model;
using FinancialManager.Domain.Entity;
using FinancialManager.Domain.Enum;
using FinancialManager.Domain.Repository;

namespace FinancialManager.Application.Usecase.CreateTransaction
{
    public sealed record CreateTransactionCommand(CreateTransactionModel request) : ICommand<Guid>;

    public sealed class CreateTransactionCommandHandler : ICommandHandler<CreateTransactionCommand, Guid>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTransactionCommandHandler(ITransactionRepository transactionRepository, IUnitOfWork unitOfWork)
        {
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateTransactionCommand command, CancellationToken cancellationToken)
        {
            if (command.request.Type > 2) throw new Exception("Invalid Transaction Type");
            var transactionType = command.request.Type == 1 ? TransactionType.Credit : TransactionType.Deposit;
            var transaction = Transaction.Create(command.request.Author, command.request.Amount, command.request.Date, transactionType, command.request.Description);

            await _transactionRepository.SaveAsync(transaction);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return transaction.Id;
        }
    }
}
