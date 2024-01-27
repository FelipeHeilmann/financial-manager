using FinancialManager.Application.Abstraction;
using FinancialManager.Domain.Abstraction;
using FinancialManager.Domain.Exception;
using FinancialManager.Domain.Repository;

namespace FinancialManager.Application.Usecase.Installment.GetInstallmentById
{
    public sealed class GetInstallmentByIdQueryHandler : IQueryHandler<GetInstallmentByIdQuery, Domain.Entity.Installment>
    {
        private readonly IInstallmentRepository _installmentRepository;

        public GetInstallmentByIdQueryHandler(IInstallmentRepository installmentRepository)
        {
            _installmentRepository = installmentRepository;
        }

        public async Task<Result<Domain.Entity.Installment>> Handle(GetInstallmentByIdQuery query, CancellationToken cancellationToken)
        {
            var installment = await _installmentRepository.GetByIdAsync(query.id, cancellationToken);
            if (installment == null)
            {
                return Result.Failure<Domain.Entity.Installment>(InstallmentErrors.NotFound);
            }

            return Result.Success(installment);
        }
    }
}
