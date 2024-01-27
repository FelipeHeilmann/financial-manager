using FinancialManager.Domain.Entity;
using FinancialManager.Domain.Repository;
using FinancialManager.Infra.Context;

namespace FinancialManager.Infra.Repository;
public class InstallmentRepository : Repository<Installment>, IInstallmentRepository
{
    public InstallmentRepository(ApplicationContext context)
        : base(context) { }
}
