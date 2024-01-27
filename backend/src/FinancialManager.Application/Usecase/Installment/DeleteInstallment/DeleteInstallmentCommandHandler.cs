using FinancialManager.Application.Abstraction;
using FinancialManager.Application.Data;
using FinancialManager.Domain.Abstraction;
using FinancialManager.Domain.Exception;
using FinancialManager.Domain.Repository;

namespace FinancialManager.Application.Usecase.Installment.DeleteInstallment;

public sealed class DeleteInstallmentCommandHandler : ICommandHandler<DeleteInstallmentCommand, Result>
{
    private readonly IInstallmentRepository _installmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteInstallmentCommandHandler(IInstallmentRepository installmentRepository, IUnitOfWork unitOfWork)
    {
        _installmentRepository = installmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteInstallmentCommand command, CancellationToken cancellationToken)
    {
       var installment = await _installmentRepository.GetByIdAsync(command.id, cancellationToken);

        if (installment == null)
        {
            return Result.Failure(InstallmentErrors.NotFound);
        }

        _installmentRepository.Delete(installment, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);

        return Result.Success();
    }
}
