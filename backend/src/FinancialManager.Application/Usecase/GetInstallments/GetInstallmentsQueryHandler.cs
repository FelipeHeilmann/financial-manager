using FinancialManager.Application.Abstraction;
using FinancialManager.Domain.Abstraction;
using FinancialManager.Domain.Entity;
using FinancialManager.Domain.Repository;

namespace FinancialManager.Application.Usecase.GetInstallments
{
    public sealed class GetInstallmentsQueryHandler : IQueryHandler<GetInstallmentsQuery, List<Installment>>
    {
        private readonly IInstallmentRepository _installmentRepository;

        public GetInstallmentsQueryHandler(IInstallmentRepository installmentRepository)
        {
            _installmentRepository = installmentRepository;
        }

        public async Task<Result<List<Installment>>> Handle(GetInstallmentsQuery query, CancellationToken cancellationToken)
        {
            var installments = await _installmentRepository.GetAllAsync(cancellationToken);

            return Result.Success(installments);
        }
    }
}
