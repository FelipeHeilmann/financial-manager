using FinancialManager.Application.Abstraction;
using FinancialManager.Domain.Abstraction;
using FinancialManager.Domain.Entity;
using FinancialManager.Domain.Exception;
using FinancialManager.Domain.Repository;

namespace FinancialManager.Application.Usecase.GetInstallmentById
{
    public sealed class GetInstallmentByIdQueryHandler : IQueryHandler<GetInstallmentByIdQuery, Installment>
    {
        private readonly IInstallmentRepository _installmentRepository;

        public GetInstallmentByIdQueryHandler(IInstallmentRepository installmentRepository)
        {
            _installmentRepository = installmentRepository;
        }

        public async Task<Result<Installment>> Handle(GetInstallmentByIdQuery query, CancellationToken cancellationToken)
        {
            var installment = await _installmentRepository.GetByIdAsync(query.id, cancellationToken);
            if(installment  == null)
            {
                return Result.Failure<Installment>(InstallmentErrors.NotFound);
            }

            return Result.Success(installment);
        }
    }
}
