using FinancialManager.Domain.Entity;

namespace FinancialManager.Domain.Repository;
public interface ITransactionRepository : IGenericRepostory<Transaction>
{
    public void AddInstallment(Installment installment, CancellationToken cancellationToken);
}

