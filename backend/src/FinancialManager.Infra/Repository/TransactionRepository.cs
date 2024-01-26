using FinancialManager.Domain.Entity;
using FinancialManager.Domain.Repository;
using FinancialManager.Infra.Context;

namespace FinancialManager.Infra.Repository
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ApplicationContext context)
            : base(context)
        {   
        }

        public override void Update(Transaction entity, CancellationToken cancellationToken)
        {
            _context.Transaction.Update(entity);
            foreach (var installment in entity.Installments)
            {
                _context.Installment.Add(installment);
            }
        }
    }
}
