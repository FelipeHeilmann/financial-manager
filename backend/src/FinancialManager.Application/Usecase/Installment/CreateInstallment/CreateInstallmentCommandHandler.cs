using FinancialManager.Application.Abstraction;
using FinancialManager.Application.Data;
using FinancialManager.Domain.Abstraction;
using FinancialManager.Domain.Exception;
using FinancialManager.Domain.Repository;

namespace FinancialManager.Application.Usecase.Installment.CreateInstallment;
public sealed class CreateInstallmentCommandHandler : ICommandHandler<CreateInstallmentCommand, Result<Guid>>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateInstallmentCommandHandler(ITransactionRepository transactionRepository, IUnitOfWork unitOfWork)
    {
        _transactionRepository = transactionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateInstallmentCommand command, CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepository.GetByIdAsync(command.request.TransactionId, cancellationToken, "Installments");

        if (transaction == null)
        {
            return Result.Failure<Guid>(TransactionErrors.NotFound);
        }

        if (command.request.Amount <= 0) return Result.Failure<Guid>(InstallmentErrors.InvalidAmount);

        var installment = Domain.Entity.Installment.Create(command.request.Amount, command.request.Date, transaction.Id);

        Result result = transaction.AddInstallmalent(installment);

        if (result.IsFailure) return Result.Failure<Guid>(result.GetError());

        _transactionRepository.AddInstallment(installment, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);

        return Result.Success(transaction.Id);
    }
}

