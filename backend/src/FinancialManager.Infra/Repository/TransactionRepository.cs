using FinancialManager.Domain.Entity;
using FinancialManager.Domain.Repository;
using FinancialManager.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace FinancialManager.Infra.Repository
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ApplicationContext context)
            : base(context) {}

        public async override void Update(Transaction entity, CancellationToken cancellationToken)
        {
            _context.Transaction.Update(entity);
            foreach (var installment in entity.Installments)
            {
                if(await _context.Installment.FirstOrDefaultAsync(i => i.Id == installment.Id) == null)
                    _context.Installment.Add(installment);
            }
        }
    }
}
